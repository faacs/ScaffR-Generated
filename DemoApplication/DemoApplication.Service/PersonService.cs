namespace DemoApplication.Service
{
    using Core.Interfaces.Data;
    using Core.Interfaces.Service;
    using Core.Model;

    public partial class PersonService : BaseService<Person>, IPersonService
    {
		protected new IPersonRepository Repository;				
		
		public PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository)
			:base(unitOfWork)
		{
		    base.Repository = Repository = personRepository;
		}		
	}
}