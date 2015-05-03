﻿/*
 * This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using ServerFramework.Configuration;
using ServerFramework.Constants.Misc;
using ServerFramework.Database;
using ServerFramework.Database.Context;
using ServerFramework.Database.Model;
using ServerFramework.Managers.Base;
using System;
using System.Text;
using System.Threading;

namespace ServerFramework.Managers.Core
{
    public sealed class LogManager : LogManagerBase<LogManager>
    {
        #region Fields

        private Timer _timer;

        #endregion

        #region Constructors

        LogManager()
        {
            Init();
        }

        #endregion

		#region Methods

		#region Init

		internal override void Init()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Thread logThread = new Thread(() =>
            {
                while (true)
                {
                    var item = ConsoleLogQueue.Take();

                    if (item != null)
                    {
                        try
                        {
                            Console.ForegroundColor = item.Item1;
                            Console.WriteLine(item.Item2);
                        }
                        catch (NullReferenceException) { }
                    }
                }
            });

            _timer = new Timer(TimerCallback, null, 10000, 10000);
            logThread.IsBackground = true;
            logThread.Start();
        }

        #endregion

        #region Message

        protected override void Message(LogType type, string message, params object[] args)
        {
            ConsoleColor color;

            switch (type)
            {
                case LogType.Normal:
                    color = ConsoleColor.Green;
                    message = message.Insert(0, "System: ");
                    break;
                case LogType.Error:
                    color = ConsoleColor.Red;
                    message = message.Insert(0, "Error: ");
                    break;
                case LogType.Init:
                    color = ConsoleColor.Cyan;
                    break;
                case LogType.Database:
                    color = ConsoleColor.Yellow;
                    break;
                case LogType.Debug:
                    message = message.Insert(0, "Debug: ");
                    color = ConsoleColor.DarkRed;
                    break;
                case LogType.Dump:
                    color = ConsoleColor.DarkMagenta;
                    break;
                case LogType.Cmd:
                    color = ConsoleColor.Gray;
                    break;
                case LogType.Command:
                    color = ConsoleColor.Blue;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }

            if ((ServerConfig.LogLevel & type) == type)
            {
                string msg = String.Empty; 

                if (type == LogType.Debug || type == LogType.Command || type == LogType.Cmd)
                {
                    msg = String.Format(message, args);
                }
                else
                {
                    msg = String.Format
                        (
                            "[{0}] {1}", DateTime.Now.ToString("HH:mm:ss.fff")
                        , String.Format(message, args)
                        );

                    LogModel logModel = new LogModel();
                    logModel.LogTypeID = (int)type;
                    logModel.Message = String.Format(message, args);

                    LogList.Add(logModel);
                }

                ConsoleLogQueue.Add(Tuple.Create<ConsoleColor, string>(color, msg));
            }

        }

        #endregion

        #region Log

        public override void Log(LogType type, string message, params object[] args)
        {
            Message(type, message, args);
        }

        public void Log(string message, params object[] args)
        {
            Log(LogType.Cmd, message, args);
        }

        public void Log(string message)
        {
            Log(LogType.Cmd, message);
        }

        public void Log()
        {
            Log(LogType.Cmd, "");
        }

        #endregion

        #region TimerCallback

        private void TimerCallback(object o)
        {
            if (LogList.Count > 0)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.Log.AddRange(LogList);
                    context.SaveChanges();
                }

                LogList.Clear();
            }
        }

        #endregion

        #endregion
    }
}