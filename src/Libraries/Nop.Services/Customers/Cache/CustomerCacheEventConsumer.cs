using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Services.Events;

namespace Nop.Services.Customers.Cache
{
    /// <summary>
    /// Customer cache event consumer (used for caching of current customer password)
    /// </summary>
    public partial class CustomerCacheEventConsumer : IConsumer<CustomerPasswordChangedEvent>
    {
        #region Constants

        /// <summary>
        /// Key for current customer password
        /// </summary>
        /// <remarks>
        /// {0} : customer identifier
        /// </remarks>
        public const string CUSTOMER_CURRENT_PASSWORD = "Nop.customers.currentpassword-{0}";
        public const string CUSTOMER_CURRENT_PASSWORD_PATTERN_KEY = "Nop.customers.currentpassword";

        #endregion

        #region Fields

        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public CustomerCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            this._cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        }

        #endregion

        #region Methods

        //password changed
        public void HandleEvent(CustomerPasswordChangedEvent eventMessage)
        {
            _cacheManager.Remove(string.Format(CUSTOMER_CURRENT_PASSWORD, eventMessage.Password.CustomerId));
        }

        #endregion
    }
}
