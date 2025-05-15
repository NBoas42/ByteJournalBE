using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase {

    private readonly AccountService _AccountService;

    public AccountController(AccountService accountService) {
        this._AccountService = accountService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAccount([FromBody] Account account) {
        Account result = await this._AccountService.CreateAccount(account);

        return Ok(result);
    }


    [HttpPut()]
    public async Task<IActionResult> UpdateAccount([FromBody] Account account) {
        Account result = await this._AccountService.UpdateAccount(account);

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAccounts([FromBody] Account account) {
        Account[] results = await this._AccountService.SearchAccounts(account);

        return Ok(results);
    }


    [HttpGet()]
    public async Task<IActionResult> GetAccountById([FromQuery] Guid accountId) {
        Account result = await this._AccountService.GetAccountByID(accountId);

        return Ok(result);
    }
    

    [HttpDelete()]
    public async Task<IActionResult> DeleteAccountById([FromQuery] Guid accountId) {
        Account result = await this._AccountService.DeleteAccountById(accountId);

        return Ok(result);
    }

}