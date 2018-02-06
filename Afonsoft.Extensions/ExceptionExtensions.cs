using System;
using System.Diagnostics;
using System.IO;
using System.Text;

/// <summary>
/// Classe com extenção para EXCEPTION 
/// AFONSO DUTRA NOGUEIRA FILHO
/// </summary>
public static class ExceptionExtensions
{
    private static readonly object Locker = new object();

    /// <summary>
    /// Tratar a Exception para recuperar um string com todos o erro detalhado.
    /// </summary>
    public static string Treatment(this Exception exception)
    {
        string msgRetorno = "";

        string exceptionMessages = string.Empty;
        string stackTraces = string.Empty;
        Exception tmpException = exception;

        if (tmpException != null)
        {
            while (tmpException != null)
            {
                exceptionMessages += tmpException.Message + Environment.NewLine;
                string traces = "";
                var trace = new StackTrace(tmpException, true);
                var stackFrames = trace.GetFrames();
                if (stackFrames != null)
                    foreach (StackFrame stack in stackFrames)
                    {
                        if (stack.GetFileLineNumber() > 0)
                        {
                            traces += "FileName: " + stack.GetFileName() + Environment.NewLine;
                            traces += "Metodo: " + stack.GetMethod().Name + Environment.NewLine;
                            traces += "Line: " + stack.GetFileLineNumber() + Environment.NewLine;
                            traces += "Column: " + stack.GetFileColumnNumber() + Environment.NewLine + Environment.NewLine;
                        }
                    }
                if (!String.IsNullOrEmpty(traces))
                    stackTraces += tmpException.StackTrace + Environment.NewLine + Environment.NewLine + traces + Environment.NewLine + Environment.NewLine;
                else
                    stackTraces += tmpException.StackTrace + Environment.NewLine + Environment.NewLine;

                tmpException = tmpException.InnerException;
            }
            msgRetorno = "Messages: " + exceptionMessages + Environment.NewLine + " StackTraces: " + stackTraces;

            Trace.WriteLine(msgRetorno);
            Debug.WriteLine(msgRetorno);
        }
        return msgRetorno;
    }
}