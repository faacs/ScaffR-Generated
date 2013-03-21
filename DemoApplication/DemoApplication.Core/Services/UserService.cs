#region credits
// ***********************************************************************
// Assembly	: DemoApplication.Core
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-19-2013
// ***********************************************************************
#endregion
namespace DemoApplication.Core.Services
{
    #region

    using Interfaces.Data;
    using Interfaces.Eventing;
    using Interfaces.Membership;
    using Interfaces.Service;
    using Model;

    #endregion

    public partial class UserService : BaseService<User>, IUserService      
    {
		protected new IUserRepository Repository;
        private readonly IMembershipSettings _membershipSettings;
        private IMessageBus _messageBus;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMembershipSettings membershipSettings, IMessageBus messageBus)
			:base(unitOfWork)
        {
            _messageBus = messageBus;
            _membershipSettings = membershipSettings;
            base.Repository = Repository = userRepository;
        }
    }
}