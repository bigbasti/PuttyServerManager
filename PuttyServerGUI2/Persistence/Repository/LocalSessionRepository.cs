using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuttyServerGUI2.Persistence.Repository {
    class LocalSessionRepository : ISessionRepository {

        public void AddSession(string filename) {
            throw new NotImplementedException();
        }

        public void AddSession(string server, string protocol, int port, string name = "") {
            throw new NotImplementedException();
        }

        public bool CheckSessionExists(string sessionName) {
            throw new NotImplementedException();
        }

        public void RenameSession(string sessionName, string newName) {
            throw new NotImplementedException();
        }

        public void DeleteSession(string sessionName) {
            throw new NotImplementedException();
        }


    }
}
