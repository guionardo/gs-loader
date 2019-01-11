using gs_loader_common.Interfaces;
using gs_loader_common.Repository;
using gs_loader_common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Run
{
    public static class DoInstall
    {
        public static bool Install(string basePath, string repositoryHost, string programName, out string message)
        {
            message = "";
            RepositoryInfo repo = new RepositoryInfo(repositoryHost);
            if (repo.RepositoryType == RepositoryType.None)
            {

            }
            IRepository repository;
            switch (repo.RepositoryType)
            {
                case RepositoryType.HTTP:
                    repository = new HTTPRepository(repositoryHost);
                    break;
                case RepositoryType.Folder:
                    repository = new FolderRepository(repositoryHost);
                    break;

                default:
                    message = Strings.Get(StringName.OptionErrorRepositoryType, "REPO", repositoryHost);
                    return false;
            }

            return true;
            //TODO: Implementar a instalação de um programa a partir de um repositório
        }
    }

}
