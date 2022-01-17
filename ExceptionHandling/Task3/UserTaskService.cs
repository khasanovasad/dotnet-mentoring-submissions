using System;
using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "Invalid userId");
            }

            var user = _userDao.GetUser(userId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Attempt to add already existent task");
                }
            }

            tasks.Add(task);
        }
    }
}