using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shareboard.Networking
{
	class Client
	{
		#region Attributes

		private Socket _socket;

		#endregion

		#region Constructors

		public Client()
		{
			this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		#endregion
	}
}
