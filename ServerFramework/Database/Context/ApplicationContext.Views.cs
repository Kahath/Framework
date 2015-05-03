//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(ServerFramework.Database.Context.ApplicationContext),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets82cc21be635add3e8c49e9e2ef42717041c0f6219d32c671947707eec1998812))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework Power Tools", "0.9.0.0")]
    internal sealed class ViewsForBaseEntitySets82cc21be635add3e8c49e9e2ef42717041c0f6219d32c671947707eec1998812 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "82cc21be635add3e8c49e9e2ef42717041c0f6219d32c671947707eec1998812"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "CodeFirstDatabase.CommandModel")
            {
                return GetView0();
            }

            if (extentName == "ApplicationContext.Commands")
            {
                return GetView1();
            }

            if (extentName == "CodeFirstDatabase.LogModel")
            {
                return GetView2();
            }

            if (extentName == "CodeFirstDatabase.LogTypeModel")
            {
                return GetView3();
            }

            if (extentName == "ApplicationContext.Log")
            {
                return GetView4();
            }

            if (extentName == "ApplicationContext.LogType")
            {
                return GetView5();
            }

            if (extentName == "CodeFirstDatabase.PacketLogModel")
            {
                return GetView6();
            }

            if (extentName == "CodeFirstDatabase.PacketLogTypeModel")
            {
                return GetView7();
            }

            if (extentName == "ApplicationContext.PacketLog")
            {
                return GetView8();
            }

            if (extentName == "ApplicationContext.PacketLogType")
            {
                return GetView9();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.CommandModel.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CommandModel
        [CodeFirstDatabaseSchema.CommandModel](T1.CommandModel_ID, T1.CommandModel_Name, T1.CommandModel_CommandLevel, T1.CommandModel_Description, T1.CommandModel_Active, T1.CommandModel_DateCreated, T1.CommandModel_DateModified, T1.CommandModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS CommandModel_ID, 
            T.Name AS CommandModel_Name, 
            T.CommandLevel AS CommandModel_CommandLevel, 
            T.Description AS CommandModel_Description, 
            T.Active AS CommandModel_Active, 
            T.DateCreated AS CommandModel_DateCreated, 
            T.DateModified AS CommandModel_DateModified, 
            T.DateDeactivated AS CommandModel_DateDeactivated, 
            True AS _from0
        FROM ApplicationContext.Commands AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for ApplicationContext.Commands.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Commands
        [ServerFramework.Database.Context.CommandModel](T1.CommandModel_ID, T1.CommandModel_Name, T1.CommandModel_CommandLevel, T1.CommandModel_Description, T1.CommandModel_Active, T1.CommandModel_DateCreated, T1.CommandModel_DateModified, T1.CommandModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS CommandModel_ID, 
            T.Name AS CommandModel_Name, 
            T.CommandLevel AS CommandModel_CommandLevel, 
            T.Description AS CommandModel_Description, 
            T.Active AS CommandModel_Active, 
            T.DateCreated AS CommandModel_DateCreated, 
            T.DateModified AS CommandModel_DateModified, 
            T.DateDeactivated AS CommandModel_DateDeactivated, 
            True AS _from0
        FROM CodeFirstDatabase.CommandModel AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.LogModel.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView2()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing LogModel
        [CodeFirstDatabaseSchema.LogModel](T1.LogModel_ID, T1.LogModel_Message, T1.LogModel_LogTypeID, T1.LogModel_Active, T1.LogModel_DateCreated, T1.LogModel_DateModified, T1.LogModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS LogModel_ID, 
            T.Message AS LogModel_Message, 
            T.LogTypeID AS LogModel_LogTypeID, 
            T.Active AS LogModel_Active, 
            T.DateCreated AS LogModel_DateCreated, 
            T.DateModified AS LogModel_DateModified, 
            T.DateDeactivated AS LogModel_DateDeactivated, 
            True AS _from0
        FROM ApplicationContext.Log AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.LogTypeModel.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView3()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing LogTypeModel
        [CodeFirstDatabaseSchema.LogTypeModel](T1.LogTypeModel_ID, T1.LogTypeModel_Name, T1.LogTypeModel_Active, T1.LogTypeModel_DateCreated, T1.LogTypeModel_DateModified, T1.LogTypeModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS LogTypeModel_ID, 
            T.Name AS LogTypeModel_Name, 
            T.Active AS LogTypeModel_Active, 
            T.DateCreated AS LogTypeModel_DateCreated, 
            T.DateModified AS LogTypeModel_DateModified, 
            T.DateDeactivated AS LogTypeModel_DateDeactivated, 
            True AS _from0
        FROM ApplicationContext.LogType AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for ApplicationContext.Log.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView4()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Log
        [ServerFramework.Database.Context.LogModel](T1.LogModel_ID, T1.LogModel_Message, T1.LogModel_LogTypeID, T1.LogModel_Active, T1.LogModel_DateCreated, T1.LogModel_DateModified, T1.LogModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS LogModel_ID, 
            T.Message AS LogModel_Message, 
            T.LogTypeID AS LogModel_LogTypeID, 
            T.Active AS LogModel_Active, 
            T.DateCreated AS LogModel_DateCreated, 
            T.DateModified AS LogModel_DateModified, 
            T.DateDeactivated AS LogModel_DateDeactivated, 
            True AS _from0
        FROM CodeFirstDatabase.LogModel AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for ApplicationContext.LogType.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView5()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing LogType
        [ServerFramework.Database.Context.LogTypeModel](T1.LogTypeModel_ID, T1.LogTypeModel_Name, T1.LogTypeModel_Active, T1.LogTypeModel_DateCreated, T1.LogTypeModel_DateModified, T1.LogTypeModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS LogTypeModel_ID, 
            T.Name AS LogTypeModel_Name, 
            T.Active AS LogTypeModel_Active, 
            T.DateCreated AS LogTypeModel_DateCreated, 
            T.DateModified AS LogTypeModel_DateModified, 
            T.DateDeactivated AS LogTypeModel_DateDeactivated, 
            True AS _from0
        FROM CodeFirstDatabase.LogTypeModel AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.PacketLogModel.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView6()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing PacketLogModel
        [CodeFirstDatabaseSchema.PacketLogModel](T1.PacketLogModel_ID, T1.PacketLogModel_IP, T1.PacketLogModel_ClientID, T1.PacketLogModel_Size, T1.PacketLogModel_PacketLogTypeID, T1.PacketLogModel_Opcode, T1.PacketLogModel_Message, T1.PacketLogModel_Active, T1.PacketLogModel_DateCreated, T1.PacketLogModel_DateModified, T1.PacketLogModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS PacketLogModel_ID, 
            T.IP AS PacketLogModel_IP, 
            T.ClientID AS PacketLogModel_ClientID, 
            T.Size AS PacketLogModel_Size, 
            T.PacketLogTypeID AS PacketLogModel_PacketLogTypeID, 
            T.Opcode AS PacketLogModel_Opcode, 
            T.Message AS PacketLogModel_Message, 
            T.Active AS PacketLogModel_Active, 
            T.DateCreated AS PacketLogModel_DateCreated, 
            T.DateModified AS PacketLogModel_DateModified, 
            T.DateDeactivated AS PacketLogModel_DateDeactivated, 
            True AS _from0
        FROM ApplicationContext.PacketLog AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.PacketLogTypeModel.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView7()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing PacketLogTypeModel
        [CodeFirstDatabaseSchema.PacketLogTypeModel](T1.PacketLogTypeModel_ID, T1.PacketLogTypeModel_Name, T1.PacketLogTypeModel_Active, T1.PacketLogTypeModel_DateCreated, T1.PacketLogTypeModel_DateModified, T1.PacketLogTypeModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS PacketLogTypeModel_ID, 
            T.Name AS PacketLogTypeModel_Name, 
            T.Active AS PacketLogTypeModel_Active, 
            T.DateCreated AS PacketLogTypeModel_DateCreated, 
            T.DateModified AS PacketLogTypeModel_DateModified, 
            T.DateDeactivated AS PacketLogTypeModel_DateDeactivated, 
            True AS _from0
        FROM ApplicationContext.PacketLogType AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for ApplicationContext.PacketLog.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView8()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing PacketLog
        [ServerFramework.Database.Context.PacketLogModel](T1.PacketLogModel_ID, T1.PacketLogModel_IP, T1.PacketLogModel_ClientID, T1.PacketLogModel_Size, T1.PacketLogModel_PacketLogTypeID, T1.PacketLogModel_Opcode, T1.PacketLogModel_Message, T1.PacketLogModel_Active, T1.PacketLogModel_DateCreated, T1.PacketLogModel_DateModified, T1.PacketLogModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS PacketLogModel_ID, 
            T.IP AS PacketLogModel_IP, 
            T.ClientID AS PacketLogModel_ClientID, 
            T.Size AS PacketLogModel_Size, 
            T.PacketLogTypeID AS PacketLogModel_PacketLogTypeID, 
            T.Opcode AS PacketLogModel_Opcode, 
            T.Message AS PacketLogModel_Message, 
            T.Active AS PacketLogModel_Active, 
            T.DateCreated AS PacketLogModel_DateCreated, 
            T.DateModified AS PacketLogModel_DateModified, 
            T.DateDeactivated AS PacketLogModel_DateDeactivated, 
            True AS _from0
        FROM CodeFirstDatabase.PacketLogModel AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for ApplicationContext.PacketLogType.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView9()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing PacketLogType
        [ServerFramework.Database.Context.PacketLogTypeModel](T1.PacketLogTypeModel_ID, T1.PacketLogTypeModel_Name, T1.PacketLogTypeModel_Active, T1.PacketLogTypeModel_DateCreated, T1.PacketLogTypeModel_DateModified, T1.PacketLogTypeModel_DateDeactivated)
    FROM (
        SELECT 
            T.ID AS PacketLogTypeModel_ID, 
            T.Name AS PacketLogTypeModel_Name, 
            T.Active AS PacketLogTypeModel_Active, 
            T.DateCreated AS PacketLogTypeModel_DateCreated, 
            T.DateModified AS PacketLogTypeModel_DateModified, 
            T.DateDeactivated AS PacketLogTypeModel_DateDeactivated, 
            True AS _from0
        FROM CodeFirstDatabase.PacketLogTypeModel AS T
    ) AS T1");
        }
    }
}
