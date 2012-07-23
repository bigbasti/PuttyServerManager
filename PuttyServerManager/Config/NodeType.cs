using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuttyServerManager.Config {
    /// <summary>
    /// Enum mappt die ID des Bildes aus der verknüpften ImageList zu der TreeView.
    /// </summary>
    public enum NodeType : int {
        RootNode = 0,           //Stellt die RootNode der Liste dar
        FolderNode = 1,         //Stellt einen gewöhnlichen Ordner dar
        ServerNode = 6,         //Stellt eine ausführbare Session dar
        ServerError = 9         //Stellt eine nicht gefundene Session dar
    }
}
