using System.Collections.Generic;

namespace Shareboard.Networking
{
	class Network
	{
		#region Attributes

		public List<Client> Users;

		#endregion

		#region Constructors

		public Network()
		{
			Users = new List<Client>();
		}

		public Network(List<Client> Users)
		{
			this.Users = Users;
		}

		#endregion

		
	}
}