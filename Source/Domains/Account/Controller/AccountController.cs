using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/account")]
public class AccountController : Controller {
    private readonly AccountService _AccountService;

    public AccountController(AccountService accountService) {
        this._AccountService = accountService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAccount([FromBody] Account account) {
        try {
            Account result = await this._AccountService.CreateAccount(account);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount([FromQuery] Guid id, [FromBody] AccountUpdateDTO accountToUpdate) {
        try {
            Account result = await this._AccountService.UpdateAccount(id,accountToUpdate);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAccounts([FromBody] Account account) {
        try {
            Account[] results = await this._AccountService.SearchAccounts(account);
            return StatusCode(200, results);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpGet()]
    public async Task<IActionResult> GetAccountById([FromQuery] Guid accountId) {
        try {
            Account result = await this._AccountService.GetAccountByID(accountId);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteAccountById([FromQuery] Guid accountId) {
        try {
            Account result = await this._AccountService.DeleteAccountById(accountId);
            return StatusCode(201, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }
}
