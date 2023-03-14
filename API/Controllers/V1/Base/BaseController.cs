using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Base
{
    public class BaseController : ControllerBase
    {
        protected void ThrowIfFalse(bool condition)
        {
            if (!condition)
            {
                throw new ArgumentException("Invalid arguments");
            }
        }
    }
}
