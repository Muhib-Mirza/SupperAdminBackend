using LMS.API.Extensions.Service;
using LMS.API.Logging;

namespace API.API.Extensions.Repository
{
    public static class AsyncTaskRunner
    {



        /// <summary>
        /// Runs an asynchronous task with error handling.
        /// </summary>
        /// <param name="taskFunc">The function that returns the task to be executed.</param>
        /// <param name="taskName">A name or identifier for the task for logging purposes.</param>
        /// <param name="cancellationToken">The cancellation token to signal task cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public static async Task RunAsync(Func<CancellationToken, Task> taskFunc, string taskName, CancellationToken cancellationToken = default)
        {
            try
            {
                Logger.LogInformation($"Starting task: {taskName}");

                // Run the task
                await taskFunc(cancellationToken);

                Logger.LogInformation($"Task {taskName} completed successfully.");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Proceed(ex);
            }
        }


        /// <summary>
        /// Runs an asynchronous task with a return value and error handling.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskFunc">The function that returns the task to be executed.</param>
        /// <param name="taskName">A name or identifier for the task for logging purposes.</param>
        /// <param name="cancellationToken">The cancellation token to signal task cancellation.</param>
        /// <returns>A Task representing the asynchronous operation, with a result.</returns>
        public static async Task<TResult> RunAsync<TResult>(Func<CancellationToken, Task<TResult>> taskFunc, string taskName, CancellationToken cancellationToken = default)
        {
            TResult result = default(TResult); // Initialize TResult to its default value
            try
            {
                Logger.LogInformation($"Starting task: {taskName}");

                // Run the task and get the result
                result = await taskFunc(cancellationToken);

                Logger.LogInformation($"Task {taskName} completed successfully.");

                
            }
            catch (Exception ex)
            {
                ExceptionHandler.Proceed(ex);
            }

            return result;
        }
    }
}
