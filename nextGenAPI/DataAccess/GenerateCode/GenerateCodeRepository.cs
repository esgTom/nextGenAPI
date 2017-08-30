using nextGenAPI.DataAccess.Common;
using System;
using nextGenAPI.CodeGeneration;

namespace nextGenAPI.DataAccess.GenerateCode {
    public class GenerateCodeRepository : RepositoryBase {

        public String GetGeneratedCode(string selectedTemplateName, int selectedTableId) {
            
            String generatedCode = string.Empty;
            var codeGenerator = new CodeGenerator();
            generatedCode = codeGenerator.GetCode(selectedTemplateName, selectedTableId);
                        
            return generatedCode;
        }
    }
}