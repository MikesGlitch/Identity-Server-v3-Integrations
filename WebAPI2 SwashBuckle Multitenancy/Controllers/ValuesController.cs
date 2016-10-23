namespace WebAPI2.SwashBuckle.Multitenancy.Controllers
{
    using System.Web.Http;

    [Authorize]
    public class ValuesController : ApiController
    {
        public IHttpActionResult GetValues()
        {
            return Ok(new[] { "a", "b", "c" });
        }
    }
}
