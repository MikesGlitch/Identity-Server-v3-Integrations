# Identity Server 3 Integrations

Here are some examples of integrations that I've used in the past to use Identity Server V3 for:
<ul>
<li>WebAPI Swashbuckle Auth</li>
<li>Self Hosted SignalR Auth</li>
</ul> 

This code will go alongside my article here:  [http://michaelclark.tech/2016/08/21/integrating-identity-server-3-with-web-api-2-swashbuckle/] (http://michaelclark.tech/2016/08/21/integrating-identity-server-3-with-web-api-2-swashbuckle/)


##To see the WebAPI SwashBuckle Integration:
1. Start the CustomUserService
2. Start the WebAPI2 Swashbuckle Multitenancy project
3. Type in "Demo" into the Tenant input box at the top right
4. Double click on the input box - it will take you to ID Server
5. Fill in the credentials. e.g. Username: Alice, Password: Alice
6. Authorize the application
7. Click ok and it should redirect you back to the Swashbuckle page with an access token filled in on the input box - you are now authenticated.

##To see the SignalR Integration:
