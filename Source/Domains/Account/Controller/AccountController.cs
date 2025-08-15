using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/account")]
public class AccountController : Controller {
    private readonly AccountService accountService;

    public AccountController(AccountService accountService) {
        this.accountService = accountService;
    }

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] Account account) {
        try {
            Account result = await this.accountService.Create(account);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AccountUpdateDTO accountToUpdate) {
        try {
            Account result = await this.accountService.Update(id, accountToUpdate);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpPut("{id}/update-password")]
    public async Task<IActionResult> UpdatePassword([FromRoute] Guid accountId, [FromBody] AccountPasswordUpdateDTO accountToUpdate) {
        try {
            Account result = await this.accountService.UpdatePassword( accountToUpdate.OldPassword, accountToUpdate.NewPassword, accountId);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromRoute] Guid accountId, [FromBody] AccountAuthenticateDTO accountAuthenticateRequest) {
        try {
            bool result = await this.accountService.Authenticate(accountAuthenticateRequest.Password, accountAuthenticateRequest.Email);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] AccountSearchDTO account) {
        try {
            Account[] results = await this.accountService.Search(account);
            return StatusCode(200, results);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) {
        try {
            Account result = await this.accountService.GetByID(id);
            return StatusCode(200, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteById([FromRoute] Guid accountId) {
        try {
            Account result = await this.accountService.DeleteById(accountId);
            return StatusCode(201, result);
        }
        catch (HttpException exception) {
            int statusCode = exception.StatusCode;
            return StatusCode(statusCode, exception.Message);
        }
    }
}
