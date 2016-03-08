using System;
using System.Reflection;

namespace Ryanair.Aegis.Samplepumper.WebApplication.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}