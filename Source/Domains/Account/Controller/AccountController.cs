using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly AccountResource accountResource;
    AccountController(AccountResource accountResource)
    {
        this.accountResource = accountResource;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAccounts()
    {

        Account[] results = await this.accountResource.searchAccounts();

        return Ok(results);
    }
}