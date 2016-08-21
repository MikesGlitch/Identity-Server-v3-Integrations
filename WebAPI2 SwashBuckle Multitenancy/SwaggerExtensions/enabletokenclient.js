(function ($, swaggerUi) {
    $(function () {
        var settings = {
            authority: 'https://localhost:44333/core/',
            client_id: 'web.api2.swashbuckle.multitenancy.implicit',
            redirect_uri: window.location
                .protocol +
                '//' +
                window.location.host +
                '/',

            response_type: 'id_token token',
            scope: 'openid profile email tenant',
            prompt: 'login',
            filter_protocol_claims: true
        };
        
        manager = new Oidc.UserManager(settings);

        $inputApiKey = $('#input_tenantapiKey');
        $inputApiKey.on('dblclick', function () {
            var tenant = $inputApiKey.val();
            if (tenant && tenant.trim() != "") {
                manager._settings._acr_values = 'tenant=' + $('#input_tenantapiKey').val();
                manager.signinRedirect().then(function () {
                }, function (error) {
                    console.error(error);
                });
            }
        });

        // checks to see if the page being loaded looks like a login callback
        if (window.location.hash) {
            signInCallback();
        }

        function signInCallback() {
            manager.signinRedirectCallback().then(function (user) {
                var hash = window.location.hash.substr(1);
                var result = hash.split('&').reduce(function (result, item) {
                    var parts = item.split('=');
                    result[parts[0]] = parts[1];
                    return result;
                }, {});
                console.log(result);
                $inputApiKey.val(user.access_token).change();
            }).catch(function (error) {
                console.error(error);
            });
        }
    });
})(jQuery, window.swaggerUi);