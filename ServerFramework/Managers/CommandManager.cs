﻿using ServerFramework.Constants.Attributes;
using ServerFramework.Constants.Entities.Console;
using ServerFramework.Constants.Misc;
using ServerFramework.Logging;
using ServerFramework.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ServerFramework.Managers
{
    public sealed class CommandManager : SingletonBase<CommandManager>
    {
        #region Fields

        private List<Command> _commandTable;

        #endregion

        #region Properties

        internal List<Command> CommandTable
        {
            get { return _commandTable; }
            set { _commandTable = value; }
        }

        #endregion

        #region Constructor

        CommandManager()
        {
            CommandTable = new List<Command>();
            Init();
        }

        #endregion

        #region Methods

        #region Init

        internal override void Init()
        {
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in a.GetTypes())
                {
                    foreach (var attr in type.GetCustomAttributes<CommandAttribute>())
                    {
                        if (attr != null)
                        {
                            MethodInfo method = type.GetMethod("GetCommand");

                            if (method != null)
                            {
                                Command c = null;

                                c = method.Invoke(null, null) as Command;

                                if (c != null)
                                    CommandTable.Add(c);
                            }
                        }
                    }
                }
            }

            LogManager.Log(LogType.Normal, "{0} Commands loaded", CommandTable.Count);

            base.Init();
        }

        #endregion

        #region InvokeCommand

        internal bool InvokeCommand(string command)
        {
            string com = Regex.Replace(command, @"\s+", " ").Trim();
            if(com != "")
                return _invokeCommandHandler(_commandTable.ToArray()
                    , com.Split(' ').ToList(), string.Empty);

            return false;
        }

        #endregion

        #region _invokeCommandHandler

        private bool _invokeCommandHandler(Command[] commandTable,
            List<string> command, string path)
        {
            if (commandTable == null || command == null)
                return false;

            foreach (Command c in commandTable)
            {
                if (c.Name.StartsWith(command[0].Trim()))
                {
                    if (c.Script == null)
                    {
                        if (c.SubCommands != null)
                        {
                            command.RemoveAt(0);
                            path += c.Name + " ";

                            return _invokeCommandHandler(c.SubCommands, command, path);

                        }
                        else
                        {
                            LogManager.Log(LogType.Command, "Error with '{0}{1}' command."
                                + " Missing script or subcommands", path, c.Name);
                            return false;
                        }
                    }
                    else
                    {
                        command.RemoveAt(0);
                        try
                        {
                            return c.Script.Invoke(command.ToArray());
                        }
                        catch (IndexOutOfRangeException)
                        {
                            LogManager.Log(LogType.Error, "Error with '{0}{1}' command. wrong arguments"
                                , path, c.Name);
                            return false;
                        }
                        catch (Exception)
                        {
                            LogManager.Log(LogType.Error, "Error with '{0}{1}' command. Failed to execute handler"
                                , path, c.Name);
                            return false;
                        }
                    }
                }
            }

            LogManager.Log(LogType.Command, "Command '{0}{1}' not found", path, command[0]);
            return false;
        }

        #endregion

        #region _availableSubCommands

        private string _availableSubCommands(Command c)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Command com in c.SubCommands)
            {
                sb.AppendLine(com.Name);
            }

            return sb.ToString();
        }

        #endregion
        
        #endregion
    }
}
