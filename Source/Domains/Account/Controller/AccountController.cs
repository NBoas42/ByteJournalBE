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
    public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] AccountUpdateDTO accountToUpdate) {
        try {
            Account result = await this._AccountService.UpdateAccount(id, accountToUpdate);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchAccounts([FromBody] AccountSearchDTO account) {
        try {
            Account[] results = await this._AccountService.SearchAccounts(account);
            return StatusCode(200, results);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById([FromRoute]  Guid id) {
        try {
            Account result = await this._AccountService.GetAccountByID(id);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteAccountById([FromRoute] Guid accountId) {
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
