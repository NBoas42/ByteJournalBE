using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase {
    private readonly AccountResource accountResource;
    public AccountController(AccountResource accountResource) {
        this.accountResource = accountResource;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAccounts() {

        Account[] results = await this.accountResource.searchAccounts();

        return Ok(results);
    }
}