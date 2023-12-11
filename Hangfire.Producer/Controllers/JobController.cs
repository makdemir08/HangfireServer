using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        public JobController(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        [Route(template:"fire-and-forget")]
        public Task<string> FireAndForget([FromBody] string text)
        {
            _backgroundJobClient.Enqueue(methodCall: () => Console.WriteLine(text));
            return Task.FromResult(text);
        }

        [HttpPost]
        [Route(template: "delayed")]
        public Task<string> Delayed([FromBody] string text)
        {
            _backgroundJobClient.Schedule(methodCall:()=> Console.WriteLine(text),delay:TimeSpan.FromMinutes(1));
            return Task.FromResult(text);   
        }

        [HttpPost]
        [Route(template: "continuation")]
        public Task<string> Continuation([FromBody] string text)
        {
            var jobId = _backgroundJobClient.Schedule(methodCall:()=> Console.WriteLine(text),delay:TimeSpan.FromMinutes(1));
            _backgroundJobClient.ContinueJobWith(jobId, methodCall: () => Console.WriteLine(text));
            return Task.FromResult(text);
        }
    }
}
