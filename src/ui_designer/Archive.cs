using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public enum ArchiveType
    {
        Json,
    }

    public interface IArchive
    {
        ArchiveType GetType();

        bool MatchFileType(string filePath);

        bool LoadFile(string filePath);
        bool SaveFile(string filePath);
    }

    public interface ArchiveSystem
    {
        IArchive Save(Node node, ArchiveType type);
        Node Load(IArchive arc);
    }
}
