using System;
using System.Collections.Generic;

namespace gs_loader_common.Resources
{
    public static class Strings
    {
        static Dictionary<StringName, string> strings = new Dictionary<StringName, string>();
        public static void SetCulture(string culture)
        {
            strings.Clear();
            switch (culture.ToUpperInvariant())
            {
                case "PT-BR":
                case "PT-PT":
                    strings.Add(StringName.OptionHelp, "Ajuda");
                    strings.Add(StringName.OptionRun, "Executar programa a partir da pasta de configuração");
                    strings.Add(StringName.OptionInstall, "Instalar programa a partir de repositório. Requer as opções -t e -p.");
                    strings.Add(StringName.OptionUpdate, "Atualizar programa");
                    strings.Add(StringName.OptionVerify, "Verificar arquivos do programa");
                    strings.Add(StringName.OptionStats, "Estatísticas de execução");
                    strings.Add(StringName.OptionErrorParameter, "Parâmetro inválido: %MESSAGE%\nExecute com o parâmetro --help para mais informações.");
                    strings.Add(StringName.OptionSetup, "Cria/altera configuração do programa");
                    strings.Add(StringName.OptionHelpMessage, @"Software de execução, instalação e atualização de programas
Guiosoft Informática

Utilização: %EXE% [opções]

%OPT%");
                    strings.Add(StringName.OptionErrorRepository, "Repositório não foi informado");
                    strings.Add(StringName.OptionErrorRepositoryNotFound, "Repositório não encontrado: %REPO%");
                    strings.Add(StringName.OptionErrorRepositoryType, "Repositório não identificado: %REPO%");

                    strings.Add(StringName.OptionErrorProgram, "Nome do programa não foi informado");

                    strings.Add(StringName.OptionHost, "Repositório de instalação/atualização (http://repositorio.com/caminho) or (file://computador/compartilhamento)");

                    strings.Add(StringName.FileNotFound, "Arquivo inexistente: %FILE%");
                    strings.Add(StringName.FileSizeDiff, "Tamanho do arquivo: %FILE% (esperado: %EXPECTED% -> encontrado: %REAL%)");
                    strings.Add(StringName.FileMD5Diff, "MD-5: %FILE% (esperado: %EXPECTED% -> encontrado: %REAL%)");
                    strings.Add(StringName.FileIdentical, "Arquivo OK: %FILE%");
                    strings.Add(StringName.FileExecutableNotFound, "Arquivo executável não encontrado: %FILE%");
                    strings.Add(StringName.ExecutableFile, "Arquivo executável");
                    strings.Add(StringName.ExecutableNotInformed, "Arquivo executável não informado");

                    strings.Add(StringName.DirectoryNotFound, "Pasta inexistente: %DIR%");
                    strings.Add(StringName.DirectoryNotCreated, "Pasta não foi criada: %DIR% (%ERROR%)");
                    strings.Add(StringName.FileNotCreated, "Arquivo não foi criado: %FILE% (%ERROR%)");

                    strings.Add(StringName.OptionProgram, "Programa");
                    strings.Add(StringName.FileReadMe, "ATENÇÃO-LEIA-ME.TXT");
                    break;
                default:
                    strings.Add(StringName.OptionHelp, "Help");
                    strings.Add(StringName.OptionRun, "Run program in setup folder");
                    strings.Add(StringName.OptionInstall, "Install program from repository. Requires options -t and -p.");
                    strings.Add(StringName.OptionUpdate, "Update program");
                    strings.Add(StringName.OptionVerify, "Verify program files");
                    strings.Add(StringName.OptionStats, "Running statistics");
                    strings.Add(StringName.OptionErrorParameter, "Invalida parameter: %MESSAGE%\nRun with --help parameter for more informations.");
                    strings.Add(StringName.OptionSetup, "Create/update program setup");
                    strings.Add(StringName.OptionHelpMessage, @"Running, installing and updating program software
Guiosoft Informática

Use: %EXE% [options]

%OPT%");
                    strings.Add(StringName.OptionErrorRepository, "Repository was not informed");
                    strings.Add(StringName.OptionErrorRepositoryNotFound, "Repository not found: %REPO%");
                    strings.Add(StringName.OptionErrorRepositoryType, "Repositório was not identified: %REPO%");

                    strings.Add(StringName.OptionErrorProgram, "Program name was not informed");
                    strings.Add(StringName.OptionHost, "Install/update repository  (http://repository.com/path) or (file://computer/share)");

                    strings.Add(StringName.FileNotFound, "File not found: %FILE%");
                    strings.Add(StringName.FileSizeDiff, "File length: %FILE% (expected: %EXPECTED% -> found: %REAL%)");
                    strings.Add(StringName.FileMD5Diff, "MD-5: %FILE% (expected: %EXPECTED% -> found: %REAL%)");
                    strings.Add(StringName.FileIdentical, "File OK: %FILE%");
                    strings.Add(StringName.FileExecutableNotFound, "Executable file not found: %FILE%");
                    strings.Add(StringName.ExecutableFile, "Executable file");
                    strings.Add(StringName.ExecutableNotInformed, "Executable file not informed");

                    strings.Add(StringName.DirectoryNotFound, "Directory not found: %DIR%");
                    strings.Add(StringName.DirectoryNotCreated, "Directory not created: %DIR% (%ERROR%)");
                    strings.Add(StringName.FileNotCreated, "File not created: %FILE% (%ERROR%)");

                    strings.Add(StringName.OptionProgram, "Program");
                    strings.Add(StringName.FileReadMe, "WARNING-READ-ME.TXT");
                    break;
            }
        }

        public static string Get(StringName stringName, params string[] replaces)
        {
            string ret;
            if (strings.ContainsKey(stringName))
                ret = strings[stringName];
            else
                ret = stringName.ToString();

            if (replaces.Length > 0 && replaces.Length % 2 == 0)
                for (int i = 0; i < replaces.Length; i += 2)
                    ret = ret.Replace("%" + replaces[i] + "%", replaces[i + 1]);

            return ret;
        }

        static Strings()
        {
            SetCulture(System.Globalization.CultureInfo.CurrentCulture.Name);
        }


    }
}
