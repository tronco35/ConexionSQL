using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ConexionSQL
{
    class SentenciasSQL
    {


        
        //Contructor sin parámetros
        public SentenciasSQL()
        {
            propiedadesInstancia
              = "select 'Propiedades Instancia' as Hoja," 
              + "@@SERVERNAME AS Instancia, "
              + "GETDATE() AS FechaHealhCheck, "
              + "'MemoriaUtilizada_MB' as Name, "
              + "'' as value, "
              + "(physical_memory_in_use_kb / 1024) AS value_in_use, "
              + "'' as minimum, "
              + "'' as maximum, "
              + "'Uso de memoria real actual MB' as [description] "
              + "FROM sys.dm_os_process_memory "
              + "union all "
              + "SELECT 'Propiedades Instancia' as Hoja, "
              + "@@SERVERNAME AS Instancia, "
              + "GETDATE() AS FechaHealhCheck, "
              + "name, "
              + "value, "
              + "value_in_use, "
              + "minimum, "
              + "maximum, "
              + "[description] "
              + "FROM sys.configurations WITH(NOLOCK) "
              + "WHERE NAME IN('backup compression default', "
              + "'optimize for ad hoc workloads', "
              + "'cost threshold for parallelism', "
              + "'filestream access level', 'max degree of parallelism', "
              + "'max server memory (MB)', "
              + "'min server memory (MB)', "
              + "'remote access', 'remote admin connections')";

            PropiedadesServidor 
            = "SELECT 'Propiedades Servidor' as Hoja, " +
            "GETDATE() AS FechaHealhCheck, " +
            "SERVERPROPERTY('MachineName') AS[MachineName], " +
            "SERVERPROPERTY('ServerName') AS[ServerName], " +
            "SERVERPROPERTY('InstanceName') AS[Instance], " +
            "SERVERPROPERTY('IsClustered') AS[IsClustered],  " +
            "SUBSTRING(@@VERSION, 1, 45) AS Version, " +
            "SERVERPROPERTY('Edition') AS[Edition],  " +
            "SERVERPROPERTY('ProductVersion') AS[ProductVersion], " +
            "SERVERPROPERTY('ProductLevel') AS[ProductLevel], " +
            "SERVERPROPERTY('ProductUpdateLevel') AS[ProductUpdateLevel], " +
            "SERVERPROPERTY('Collation') AS[Collation] ";

            servicesInfo 
            = "SELECT 'SQL Server Services Info' as Hoja, "
		    + "@@SERVERNAME AS Instancia, "
		    + "GETDATE() AS FechaHealhCheck, "
            + "servicename, "
            + "startup_type_desc, status_desc, "
            + "CONVERT(DATETIME, last_startup_time) AS last_startup_time, "
            + "service_account, "
            + "is_clustered," 
            + "cluster_nodename, "
            + "[filename] "
            + "FROM sys.dm_server_services WITH(NOLOCK) OPTION(RECOMPILE)";

            contadores  
            =  "select 'Contadores' AS Contadores, 		@@SERVERNAME AS Instancia, "
            + "GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value) cntr_value,iif(avg(cntr_value) >= 90, 'good', 'bad') as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'Buffer cache hit ratio' "
            + "group by counter_name "
            + "union all "
            + "select 'Contadores' AS Contadores,      @@SERVERNAME AS Instancia, "
            + " GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value), iif (avg(cntr_value) <= 70, 'good', 'bad')  as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'CPU Usage %' "
            + "group by counter_name "
            + "union all "
            + "select 'Contadores' AS Contadores,      @@SERVERNAME AS Instancia, "
            + "GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value) , iif (avg(cntr_value) <= 500, 'good', 'bad')  as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'Lock Wait Time (ms)' "
            + "group by counter_name "
            + "union all "
            + "select 'Contadores' AS Contadores,      @@SERVERNAME AS Instancia, "
            + "GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value), iif (avg(cntr_value) <= 20, 'good', 'bad')  as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'Disk Read IO/sec' "
            + "group by counter_name "
            + "union all "
            + "select 'Contadores' AS Contadores,      @@SERVERNAME AS Instancia, "
            + "GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value), iif (avg(cntr_value) <= 20, 'good', 'bad')  as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'Disk Write IO/sec' "
            + "group by counter_name "
            + "union all "
            + "select 'Contadores' AS Contadores,         @@SERVERNAME AS Instancia, "
            + "GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value), iif (avg(cntr_value) <= 500, 'good', 'bad')  as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'Avg Wait Time (in ms)' "
            + "group by counter_name "
            + "union all "
            + " select 'Contadores' AS Contadores,      @@SERVERNAME AS Instancia, "
            + "GETDATE() AS FechaHealhCheck, counter_name, avg(cntr_value), iif (avg(cntr_value) > 300, 'good', 'bad')  as verdict "
            + "from sys.dm_os_performance_counters "
            + "where counter_name = 'Page life expectancy' "
            + "group by counter_name"; 

            infoHardware 
            = "SELECT 'Hardware info' AS Hoja, " 
            + "@@SERVERNAME AS Instancia , "
            + "GETDATE() AS FechaHealhCheck, "
            + "cpu_count AS[Logical CPU Count], "
            + "physical_memory_kb / 1024 AS[Physical Memory(MB)], "
            + "sqlserver_start_time AS[SQL Server Start Time], "
            + "virtual_machine_type_desc AS[Virtual Machine Type] "
            + "FROM sys.dm_os_sys_info WITH(NOLOCK) OPTION(RECOMPILE); ";
            
            propiedadesBD 
            = "SELECT 'Propiedades Base de Datos' AS Hoja, "
            + "@@SERVERNAME AS Instancia , "
            + "GETDATE() AS FechaHealhCheck, m.database_id, "
            + "DB_NAME(m.database_id) AS[Database Name], "
            + "m.[file_id], m.[name], "
            + "m.physical_name, "
            + "m.[type_desc], "
            + "m.state_desc, "
            + "d.recovery_model_desc AS[Recovery Model], "
            + "m.is_percent_growth, "
            + "m.growth, "
            + "CONVERT(bigint, growth / 128.0) AS[Growth in MB], "
            + "CONVERT(bigint, size / 128.0) AS[Total Size in MB], "
            + "MAX(CASE WHEN bs.[type] = 'D' THEN bs.backup_finish_date ELSE NULL END) AS[Last Full Backup], "
            + "MAX(CASE WHEN bs.[type] = 'I' THEN bs.backup_finish_date ELSE NULL END) AS[Last Differential Backup], "
            + "MAX(CASE WHEN bs.[type] = 'L' THEN bs.backup_finish_date ELSE NULL END) AS[Last Log Backup], "
            + "d.[compatibility_level] AS[DB Compatibility Level], d.page_verify_option_desc AS[Page Verify Option], "
            + "d.is_auto_create_stats_on, d.is_auto_update_stats_on, d.is_auto_update_stats_async_on, "
            + "d.is_auto_close_on, d.is_auto_shrink_on, "
            + "d.target_recovery_time_in_seconds, d.is_cdc_enabled "
            + "FROM sys.master_files as m WITH(NOLOCK) "
            + "LEFT OUTER JOIN sys.databases as d WITH(NOLOCK) ON m.database_id = d.database_id "
            + "LEFT OUTER JOIN(select[type], [database_name], MAX(backup_finish_date) AS backup_finish_date "
            + "from msdb.dbo.backupset WITH (NOLOCK) "
            + "GROUP BY[type], [database_name]) bs ON bs.[database_name] = d.[name] "
            + "GROUP BY m.database_id, m.[file_id], m.[name], m.physical_name, m.[type_desc], m.state_desc, "
            + "m.is_percent_growth, m.growth, d.recovery_model_desc, CONVERT(bigint, growth / 128.0), CONVERT(bigint, size / 128.0), "
            + "bs.backup_finish_date,d.[compatibility_level], d.page_verify_option_desc, "
            + "d.is_auto_create_stats_on, d.is_auto_update_stats_on, d.is_auto_update_stats_async_on, "
            + "d.is_auto_close_on, d.is_auto_shrink_on, "
            + "d.target_recovery_time_in_seconds, d.is_cdc_enabled "
            + "ORDER BY growth DESC";

            autoCrecimiento 
            = "BEGIN TRY "
            + "IF(SELECT CONVERT(INT, value_in_use) FROM sys.configurations WHERE NAME = 'default trace enabled') = 1 "
            + "BEGIN "
            + "DECLARE @curr_tracefilename VARCHAR(500); "
            + "DECLARE @base_tracefilename VARCHAR(500); "
            + "DECLARE @indx INT; "
            + "SELECT @curr_tracefilename = path FROM sys.traces WHERE is_default = 1; "
            + "SET @curr_tracefilename = REVERSE(@curr_tracefilename); "
            + "SELECT @indx = PATINDEX('%\\%', @curr_tracefilename); " //se cambio "\" por "\\"--por error "secuencia de escape no reconocida"
            + "SET @curr_tracefilename = REVERSE(@curr_tracefilename); "
            + "SET @base_tracefilename = LEFT(@curr_tracefilename, LEN(@curr_tracefilename) - @indx) + '\\log.trc'; " //se cambio "\" por "\\"--por error "secuencia de escape no reconocida"
            + "SELECT "
            + "'Autocrecimiento' AS Hoja, "
            + "GETDATE() AS FechaHealhCheck, "
            + "ServerName AS[SQL_Instance], "
            + "DatabaseName AS[Database_Name], "
            + "Filename AS[Logical_File_Name], "
            + "(Duration / 1000) AS[Duration_MS], "
            + "StartTime AS[Start_Time], "
            + "CAST((IntegerData * 8.0 / 1024) AS DECIMAL(19, 2)) AS[Change_In_Size_MB] "
            + "FROM::fn_trace_gettable(@base_tracefilename, default) "
            + "WHERE "
            + "EventClass >= 92 "
            + "AND EventClass <= 95 "
            + "ORDER BY DatabaseName, StartTime DESC "
            + "END "
            + "ELSE "
            + "SELECT - 1 AS l1, "
            + "0 AS EventClass, "
            + "0 DatabaseName, "
            + "0 AS Filename, "
            + "0 AS Duration, "
            + "0 AS StartTime, "
            + "0 AS EndTime, "
            + "0 AS ChangeInSize "
            + "END TRY "
            + "BEGIN CATCH "
            + "SELECT - 100 AS l1, "
            + "ERROR_NUMBER() AS EventClass, "
            + "ERROR_SEVERITY() DatabaseName, "
            + "ERROR_STATE() AS Filename, "
            + "ERROR_MESSAGE() AS Duration, "
            + "1 AS StartTime, "
            + "1 AS EndTime, "
            + "1 AS ChangeInSize "
            + "END CATCH";

            UsoCPUBD 
            = "IF OBJECT_ID (N'tempdb..#Temp_DB_CPU_Stats') IS NOT NULL "
            + "DROP TABLE #Temp_DB_CPU_Stats; "

            + "WITH DB_CPU_Stats "
            + "AS "
            + "(SELECT pa.DatabaseID, "
            + "@@SERVERNAME AS Instancia, "
            + "DB_Name(pa.DatabaseID) AS[Database Name], "
            + "SUM(qs.total_worker_time / 1000) AS[CPU_Time_Ms] "
            + "FROM sys.dm_exec_query_stats AS qs WITH(NOLOCK) "
            + "CROSS APPLY(SELECT CONVERT(int, value) AS[DatabaseID] "
            + "FROM sys.dm_exec_plan_attributes(qs.plan_handle) "
            + "WHERE attribute = N'dbid') AS pa "
            + "GROUP BY DatabaseID "
            + ") "
            + "SELECT 'Uso de CPU por BD' as Hoja, "
            + "GETDATE() AS FechaHealhCheck, "
            + "ROW_NUMBER() OVER(ORDER BY[CPU_Time_Ms] DESC) AS[CPU Rank], DatabaseID, "
            + "@@SERVERNAME AS Instancia, "
            + "[Database Name], "
            + "[CPU_Time_Ms] AS[CPU Time(ms)], "
            + "CAST([CPU_Time_Ms] * 1.0 / SUM([CPU_Time_Ms]) OVER() * 100.0 AS DECIMAL(5, 2)) AS[CPU Percent] "
            + "INTO #Temp_DB_CPU_Stats "
            + "FROM DB_CPU_Stats "
            + "WHERE DatabaseID <> 32767 "
            + "ORDER BY[CPU Rank] OPTION(RECOMPILE); "

            + "select * from #Temp_DB_CPU_Stats ";

            UsoDiscoBD
            = "IF OBJECT_ID (N'tempdb..#Temp_DB_CPU_Stats') IS NOT NULL "
            + "DROP TABLE #Temp_DB_CPU_Stats; "

            + "WITH DB_CPU_Stats "
            + "AS "
            + "(SELECT pa.DatabaseID, "
            + "@@SERVERNAME AS Instancia, "
            + "DB_Name(pa.DatabaseID) AS[Database Name], "
            + "SUM(qs.total_worker_time / 1000) AS[CPU_Time_Ms] "
            + "FROM sys.dm_exec_query_stats AS qs WITH(NOLOCK) "
            + "CROSS APPLY(SELECT CONVERT(int, value) AS[DatabaseID] "
            + "FROM sys.dm_exec_plan_attributes(qs.plan_handle) "
            + "WHERE attribute = N'dbid') AS pa "
            + "GROUP BY DatabaseID "
            + ") "
            + "SELECT 'Uso de CPU por BD' as Hoja, "
            + "GETDATE() AS FechaHealhCheck, "
            + "ROW_NUMBER() OVER(ORDER BY[CPU_Time_Ms] DESC) AS[CPU Rank], DatabaseID, "
            + "@@SERVERNAME AS Instancia, "
            + "[Database Name], "
            + "[CPU_Time_Ms] AS[CPU Time(ms)], "
            + "CAST([CPU_Time_Ms] * 1.0 / SUM([CPU_Time_Ms]) OVER() * 100.0 AS DECIMAL(5, 2)) AS[CPU Percent] "
            + "INTO #Temp_DB_CPU_Stats "
            + "FROM DB_CPU_Stats "
            + "WHERE DatabaseID <> 32767; "
            +  " " 
            
            + "WITH Aggregate_IO_Statistics "
            + "AS "
            + "(SELECT DB_NAME(database_id) AS[Database Name], database_id, "
            + "CAST(SUM(num_of_bytes_read + num_of_bytes_written) / 1048576 AS DECIMAL(12, 2)) AS io_in_mb "
            + "FROM sys.dm_io_virtual_file_stats(NULL, NULL) AS[DM_IO_STATS] "
            + "GROUP BY database_id "
            + ") "
            + "SELECT 'Uso de BD' as Hoja, "
            + "GETDATE() AS FechaHealhCheck, "
            + "ROW_NUMBER() OVER(ORDER BY io_in_mb DESC) AS[I / O Rank],  database_id, "
            + "@@SERVERNAME AS Instancia,A.[Database Name], io_in_mb AS[Total I / O(MB)], "
            + "CAST(io_in_mb / SUM(io_in_mb) OVER() * 100.0 AS DECIMAL(5, 2)) AS[I / O Percent], "
            + "B.[CPU Percent], B.[CPU Time(ms)] "
            + "FROM Aggregate_IO_Statistics A INNER JOIN #Temp_DB_CPU_Stats B "
            + "ON A.[Database Name] = B.[Database Name] "
            + "ORDER BY[I / O Rank] OPTION(RECOMPILE); "; 
            
            estadisticasBD = 
             "SELECT 'Estadisticas Base de Datos' as Hoja, [vfs].[database_id],"
		    + "@@SERVERNAME AS Instancia, DB_NAME([vfs].[database_id]) [Database Name], GETDATE() AS FechaHealhCheck, "
            + "CASE "
            + "WHEN[num_of_reads] = 0 "
            + "THEN 0 "
            + "ELSE([io_stall_read_ms] /[num_of_reads]) "
	        + "END[ReadLatency], "
	        + "CASE "
            + "WHEN[io_stall_write_ms] = 0 "
            + "THEN 0 "
            + "ELSE([io_stall_write_ms] /[num_of_writes]) "
	        + "END[WriteLatency], "
	        + "CASE "
            + "WHEN([num_of_reads] = 0 AND[num_of_writes] = 0) "
            + "THEN 0 "
            + "ELSE([io_stall] / ([num_of_reads] + [num_of_writes])) "
	        + "END[Latency], "
	        + "CASE "
            + "WHEN[num_of_reads] = 0 "
            + "THEN 0 "
            + "ELSE([num_of_bytes_read] /[num_of_reads]) "
	        + "END[AvgBPerRead], "
	        + "CASE "
            + "WHEN[io_stall_write_ms] = 0 "
            + "THEN 0 "
            + "ELSE([num_of_bytes_written] /[num_of_writes]) "
	        + "END[AvgBPerWrite], "
	        + "CASE "
            + "WHEN([num_of_reads] = 0 AND[num_of_writes] = 0) "
			+ "THEN 0 "
            + "ELSE(([num_of_bytes_read] + [num_of_bytes_written]) / ([num_of_reads] + [num_of_writes])) "
            + "END[AvgBPerTransfer], "
	        + "LEFT([mf].[physical_name],2) [Drive], "	
	        + "[vfs].[database_id], "
	        + "[vfs].[file_id], "
	        + "[vfs].[sample_ms], "
	        + "[vfs].[num_of_reads], "
	        + "[vfs].[num_of_bytes_read], "
	        + "[vfs].[io_stall_read_ms], "
	        + "[vfs].[num_of_writes], "
	        + "[vfs].[num_of_bytes_written], "
	        + "[vfs].[io_stall_write_ms], "
	        + "[vfs].[io_stall], "
	        + "[vfs].[size_on_disk_bytes] / 1024 / 1024. [size_on_disk_MB], "
	        + "[vfs].[file_handle], "
	        + "[mf].[physical_name]"
            + "FROM[sys].[dm_io_virtual_file_stats](NULL, NULL) AS vfs "
            + "JOIN[sys].[master_files][mf] "
            + "ON[vfs].[database_id] = [mf].[database_id] "
            + "AND[vfs].[file_id] = [mf].[file_id] "
            + "ORDER BY[Latency] DESC; ";

            waits =
              "SELECT top (10) "
            + "'Wait Stats' as Hoja, "
            + "SERVERPROPERTY('ServerName') AS 'NOMBRE_INSTANCIA', "
            + "wait_type AS Wait_Type,  "
            + "wait_time_ms / 1000.0 AS Wait_Time_Seconds, "
            + "waiting_tasks_count AS Waiting_Tasks_Count, "
            + "wait_time_ms *100.0 / SUM(wait_time_ms) OVER() AS Percentage_WaitTime "
            + "FROM sys.dm_os_wait_stats "
            + "WHERE wait_type NOT IN "
            + "(N'BROKER_EVENTHANDLER', "
            + "N'BROKER_RECEIVE_WAITFOR', "
            + "N'BROKER_TASK_STOP', "
            + "N'BROKER_TO_FLUSH', "
            + "N'BROKER_TRANSMITTER', "
            + "N'CHECKPOINT_QUEUE', "
            + "N'CHKPT', "
            + "N'CLR_AUTO_EVENT', "
            + "N'CLR_MANUAL_EVENT', "
            + "N'CLR_SEMAPHORE', "
            + "N'DBMIRROR_DBM_EVENT', "
            + "N'DBMIRROR_DBM_MUTEX', "
            + "N'DBMIRROR_EVENTS_QUEUE', "
            + "N'DBMIRROR_WORKER_QUEUE', "
            + "N'DBMIRRORING_CMD', "
            + "N'DIRTY_PAGE_POLL', "
            + "N'DISPATCHER_QUEUE_SEMAPHORE', "
            + "N'EXECSYNC', "
            + "N'FSAGENT', "
            + "N'FT_IFTS_SCHEDULER_IDLE_WAIT', "
            + "N'FT_IFTSHC_MUTEX', "
            + "N'HADR_CLUSAPI_CALL', "
            + "N'HADR_FILESTREAM_IOMGR_IOCOMPLETION', "
            + "N'HADR_LOGCAPTURE_WAIT', "
            + "N'HADR_NOTIFICATION_DEQUEUE', "
            + "N'HADR_TIMER_TASK', "
            + "N'HADR_WORK_QUEUE', "
            + "N'LAZYWRITER_SLEEP', "
            + "N'LOGMGR_QUEUE', "
            + "N'MEMORY_ALLOCATION_EXT', "
            + "N'ONDEMAND_TASK_QUEUE', "
            + "N'PREEMPTIVE_HADR_LEASE_MECHANISM', "
            + "N'PREEMPTIVE_OS_AUTHENTICATIONOPS', "
            + "N'PREEMPTIVE_OS_AUTHORIZATIONOPS', "
            + "N'PREEMPTIVE_OS_COMOPS', "
            + "N'PREEMPTIVE_OS_CREATEFILE', "
            + "N'PREEMPTIVE_OS_CRYPTOPS', "
            + "N'PREEMPTIVE_OS_DEVICEOPS', "
            + "N'PREEMPTIVE_OS_FILEOPS', "
            + "N'PREEMPTIVE_OS_GENERICOPS', "
            + "N'PREEMPTIVE_OS_LIBRARYOPS', "
            + "N'PREEMPTIVE_OS_PIPEOPS', "
            + "N'PREEMPTIVE_OS_QUERYREGISTRY', "
            + "N'PREEMPTIVE_OS_VERIFYTRUST', "
            + "N'PREEMPTIVE_OS_WAITFORSINGLEOBJECT', "
            + "N'PREEMPTIVE_OS_WRITEFILEGATHER', "
            + "N'PREEMPTIVE_SP_SERVER_DIAGNOSTICS', "
            + "N'PREEMPTIVE_XE_GETTARGETSTATE', "
            + "N'PWAIT_ALL_COMPONENTS_INITIALIZED', "
            + "N'PWAIT_DIRECTLOGCONSUMER_GETNEXT', "
            + "N'QDS_ASYNC_QUEUE', "
            + "N'QDS_CLEANUP_STALE_QUERIES_TASK_MAIN_LOOP_SLEEP', "
            + "N'QDS_PERSIST_TASK_MAIN_LOOP_SLEEP', "
            + "N'QDS_SHUTDOWN_QUEUE', "
            + "N'REDO_THREAD_PENDING_WORK', "
            + "N'REQUEST_FOR_DEADLOCK_SEARCH', "
            + "N'RESOURCE_QUEUE', "
            + "N'SERVER_IDLE_CHECK', "
            + "N'SLEEP_BPOOL_FLUSH', "
            + "N'SLEEP_DBSTARTUP', "
            + "N'SLEEP_DCOMSTARTUP', "
            + "N'SLEEP_MASTERDBREADY', "
            + "N'SLEEP_MASTERMDREADY', "
            + "N'SLEEP_MASTERUPGRADED', "
            + "N'SLEEP_MSDBSTARTUP', "
            + "N'SLEEP_SYSTEMTASK', "
            + "N'SLEEP_TASK', "
            + "N'SP_SERVER_DIAGNOSTICS_SLEEP', "
            + "N'SQLTRACE_BUFFER_FLUSH', "
            + "N'SQLTRACE_INCREMENTAL_FLUSH_SLEEP', "
            + "N'SQLTRACE_WAIT_ENTRIES', "
            + "N'UCS_SESSION_REGISTRATION', "
            + "N'WAIT_FOR_RESULTS', "
            + "N'WAIT_XTP_CKPT_CLOSE', "
            + "N'WAIT_XTP_HOST_WAIT', "
            + "N'WAIT_XTP_OFFLINE_CKPT_NEW_LOG', "
            + "N'WAIT_XTP_RECOVERY', "
            + "N'WAITFOR', "
            + "N'WAITFOR_TASKSHUTDOWN', "
            + "N'XE_TIMER_EVENT', "
            + "N'XE_DISPATCHER_WAIT' "
            + ") AND wait_time_ms >= 1";

            sysadmin = 
              "select 'Usuarios Sysadmin' as Hoja, "
            + "SERVERPROPERTY('ServerName') AS 'NOMBRE_INSTANCIA', "
            + "l.name as NombreUsuario,  "
            + "p.type_desc as TipoUsuario, "
            + "createdate as FechaCreacion, "
            + "p.modify_date "
            + " from sys.syslogins as l inner join sys.server_principals as p ON l.sid = p.sid "
            + "where sysadmin = 1 and is_disabled = 0";

            //Scripts Health Check Base de Datos
            listBD = //Creacion de tablas BD
              "IF OBJECT_ID('tempdb..##BD') IS NOT NULL "
            + "drop table ##bd "
            + "create table ##BD "
            + "(id int identity(1, 1) primary key clustered "
            + ", BD nvarchar(50)) "
            //Insertar lista de BD
            + "insert into ##BD (bd) "
            + "select name from sys.databases where "
            + "name not in ('master', 'msdb', 'model', 'tempdb') ";
            //logica del while
            whileIni =
              "DECLARE @max INT , @min INT ,@db nvarchar (50) "
            + "SELECT @max = COUNT(BD)  FROM ##BD "
            + "SELECT @min = 1 "
            + "WHILE @min <= @max "
            + "BEGIN "
            + "select @db = BD from ##BD "
            + "where ID = @min ";

            //Creacion de tablas 
            tCantidadEjecucionesSP =
             "IF OBJECT_ID('tempdb..##CantidadEjecuciones') IS NOT NULL "
            + "drop table ##CantidadEjecuciones "
            + "CREATE TABLE ##CantidadEjecuciones( "
            + "[Hoja] [varchar](23) NOT NULL, "
            + "[Instancia] [nvarchar](128) NULL, "
            + "[BasedeDatos] [nvarchar](128) NULL, "
            + "[BasedeDatosID] [smallint] NULL, "
            + "[FechaHealhCheck] [datetime] NOT NULL, "
            + "[object_id] [int] NOT NULL, "
            + "[SCHEMA_ID] [int] NOT NULL, "
            + "[SP Name] [sysname] NOT NULL, "
            + "[Execution Count] [bigint] NOT NULL, "
            + "[Calls / Minute] [bigint] NOT NULL, "
            + "[Avg Elapsed Time] [bigint] NULL, "
            + "[Avg Worker Time] [bigint] NULL, "
            + "[Avg Logical Reads] [bigint] NULL, "
            + "[Last Execution Time] [nvarchar](4000) NULL, "
            + "[Plan Cached Time] [nvarchar](4000) NULL "
            + ") ON[PRIMARY] ";
           
            tIndicesSugeridos = 
              "IF OBJECT_ID('tempdb..##IndicesSugeridos') IS NOT NULL "
            + "drop table ##IndicesSugeridos "
            + "CREATE TABLE ##IndicesSugeridos( "
            + "[Hoja] [varchar](17) NOT NULL, "
            + "[Instancia] [nvarchar](128) NULL, "
            + "[BasedeDatos] [nvarchar](128) NULL, "
            + "[BasedeDatosID] [smallint] NULL, "
            + "[FechaHealhCheck] [datetime] NOT NULL, "
            + "[object_id] [int] NOT NULL, "
            + "[schema_id] [int] NOT NULL, "
            + "[index_advantage] [decimal](18, 2) NULL, "
            + "[last_user_seek] [datetime] NULL, "
            + "[Database.Schema.Table] [nvarchar](4000) NULL, "
            + "[equality_columns] [nvarchar](4000) NULL, "
            + "[inequality_columns] [nvarchar](4000) NULL, "
            + "[included_columns] [nvarchar](4000) NULL, "
            + "[unique_compiles] [bigint] NULL, "
            + "[user_seeks] [bigint] NULL, "
            + "[avg_total_user_cost] [decimal](18, 2) NULL, "
            + "[avg_user_impact] [decimal](18, 2) NULL, "
            + "[Table Name] [nvarchar](128) NULL, "
            + "[Table Rows] [bigint] NULL "
            + ") ON[PRIMARY]";

            tIndicesFragmentados = 
              "IF OBJECT_ID('tempdb..##INDICESFRAGMENTADOS') IS NOT NULL "
            + "drop table ##INDICESFRAGMENTADOS "
            + "CREATE TABLE ##INDICESFRAGMENTADOS( "
            + "[Hoja] [varchar](20) NOT NULL, "
            + "[BasedeDatos] [nvarchar](128) NULL, "
            + "[BasedeDatosID] [smallint] NULL, "
            + "[Instancia] [nvarchar](128) NULL, "
            + "[FechaHealhCheck] [datetime] NOT NULL, "
            + "[Database Name] [nvarchar](128) NULL, "
            + "[Schema Name] [nvarchar](128) NULL, "
            + "[OBJECT_ID] [int] NULL, "
            + "[schema_id] [int] NOT NULL, "
            + "[Object Name] [nvarchar](128) NULL, "
            + "[Index Name] [sysname] NULL, "
            + "[index_id] [int] NULL, "
            + "[index_type_desc] [nvarchar](60) NULL, "
            + "[avg_fragmentation_in_percent] [decimal](18, 2) NULL, "
            + "[fragment_count] [bigint] NULL, "
            + "[fill_factor] [tinyint] NOT NULL, "
            + "[filter_definition] [nvarchar](max)NULL, "
            + "[allow_page_locks] [bit] NULL "
            + ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] ";

            tIndicesUsados = 
              "IF OBJECT_ID('tempdb..##INDICESUSADOS') IS NOT NULL "
            + "drop table ##INDICESUSADOS "
            + "CREATE TABLE ##INDICESUSADOS( "
            + "[Hoja] [varchar](14) NOT NULL, "
            + "[BasedeDatos] [nvarchar](128) NULL, "
            + "[BasedeDatosID] [smallint] NULL, "
            + "[Instancia] [nvarchar](128) NULL, "
            + "[FechaHealhCheck] [datetime] NOT NULL, "
            + "[object_id] [int] NOT NULL, "
            + "[schema_id] [int] NOT NULL, "
            + "[index_id] [int] NOT NULL, "
            + "[is_disabled] [bit] NULL, "
            + "[esquema] [sysname] NOT NULL, "
            + "[Tabla] [sysname] NOT NULL, "
            + "[IndexName] [sysname] NULL, "
            + "[KeyCols] [nvarchar](max)NULL, "
            + "[IncludeCols] [nvarchar](max)NULL, "
            + "[user_seeks] [bigint] NOT NULL, "
            + "[user_scans] [bigint] NOT NULL, "
            + "[user_lookups] [bigint] NOT NULL, "
            + "[user_updates] [bigint] NOT NULL, "
            + "[fill_factor] [tinyint] NOT NULL "
            + ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] ";

            tDatavsIndices = 
              "IF OBJECT_ID('tempdb..##DatavsIndices') IS NOT NULL "
            + "drop table ##DatavsIndices "
            + "CREATE TABLE ##DatavsIndices( "
            + "[Hoja] [varchar](15) NOT NULL, "
            + "[FechaHealhCheck] [datetime] NOT NULL, "
            + "[Instancia] [nvarchar](128) NULL, "
            + "[BasedeDatosID] [smallint] NULL, "
            + "[BasedeDatos] [nvarchar](128) NULL, "
            + "[object_id] [int] NOT NULL, "
            + "[schema_id] [int] NOT NULL, "
            + "[name] [sysname] NOT NULL, "
            + "[Tablename] [sysname] NOT NULL, "
            + "[Row_Cnt] [bigint] NULL, "
            + "[Reserved] [bigint] NULL, "
            + "[Data] [bigint] NULL, "
            + "[Index_Size] [bigint] NULL "
            + ") ON[PRIMARY]";

            //ejecucion de consultas 
            cCantidadEjecucionesSP =
              "DECLARE @CantidadEjecuciones NVARCHAR(max) = 'use ' + @db + ' "
            + "' + ' "
            + "insert into ##CantidadEjecuciones "
            + "SELECT TOP(100) ''Cantidad Ejecuciones SP'' AS Hoja, "
            + "@@SERVERNAME AS Instancia , "
            + "db_name() as ''BasedeDatos'',  "
            + "DB_ID() as ''BasedeDatosID'', "
            + "GETDATE() AS FechaHealhCheck, "
            + "p.object_id, SCHEMA_ID, "
            + "p.name AS[SP Name], "
            + "qs.execution_count AS[Execution Count], "
            + "ISNULL(qs.execution_count / DATEDIFF(Minute, "
            + "qs.cached_time, GETDATE()), 0) AS[Calls / Minute], "
            + "qs.total_elapsed_time / qs.execution_count AS[Avg Elapsed Time], "
            + "qs.total_worker_time / qs.execution_count AS[Avg Worker Time], "
            + "qs.total_logical_reads / qs.execution_count AS[Avg Logical Reads], "
            + "FORMAT(qs.last_execution_time, ''yyyy - MM - dd HH: mm:ss'', ''en - US'') AS[Last Execution Time], "
            + "FORMAT(qs.cached_time, ''yyyy - MM - dd HH: mm:ss'', ''en - US'') AS[Plan Cached Time] "
            + "FROM sys.procedures AS p WITH(NOLOCK) "
            + "INNER JOIN sys.dm_exec_procedure_stats AS qs WITH(NOLOCK) ON p.[object_id] = qs.[object_id] "
            + "CROSS APPLY sys.dm_exec_query_plan(qs.plan_handle) AS qp "
            + "WHERE qs.database_id = DB_ID() "
            + "AND DATEDIFF(Minute, qs.cached_time, GETDATE()) > 0 "
            + "ORDER BY[Avg Logical Reads]  DESC OPTION(RECOMPILE) ' ";

            ejecutaCantidaddeEjecuciones = listBD + tCantidadEjecucionesSP + whileIni + cCantidadEjecucionesSP 
            + " begin "
            + "exec(@CantidadEjecuciones) " //NOMBRE CONSULTA DECLARADA
            + "end "
            + "SET @min = @min + 1 "
            + "end " 
            + "select* from  ##CantidadEjecuciones "; //NOMBRE TABLA TEMPORAL CONSULTA

            cIndicesSugeridos = 
              "DECLARE  @IndicesSugeridos NVARCHAR(max) = 'use ' + @db + ' "
            + "' + ' "
            + "insert into ##IndicesSugeridos "
            + "SELECT DISTINCT ''Índices Sugeridos'' as Hoja, "
		    + "@@SERVERNAME AS Instancia, "
            + "db_name() as ''BasedeDatos'', "
            + "DB_ID() as ''BasedeDatosID'', "
            + "GETDATE() AS FechaHealhCheck, "
            + "mid.object_id, "
            + "o.[schema_id], "
            + "CONVERT(decimal(18, 2), "
            + "user_seeks * avg_total_user_cost * (avg_user_impact * 0.01)) AS[index_advantage], "
            + "migs.last_user_seek, "
            + "mid.[statement] AS[Database.Schema.Table], "
            + "mid.equality_columns, "
            + "mid.inequality_columns, "
            + "mid.included_columns, "
            + "migs.unique_compiles, "
            + "migs.user_seeks, CONVERT(decimal(18, 2), migs.avg_total_user_cost) avg_total_user_cost,CONVERT(decimal(18, 2), migs.avg_user_impact) avg_user_impact, "
            + "OBJECT_NAME(mid.[object_id]) AS[Table Name], "
            + "p.rows AS[Table Rows] "
            + "FROM sys.dm_db_missing_index_group_stats AS migs WITH(NOLOCK) "
            + "INNER JOIN sys.dm_db_missing_index_groups AS mig WITH(NOLOCK)  ON migs.group_handle = mig.index_group_handle "
            + "INNER JOIN sys.dm_db_missing_index_details AS mid WITH(NOLOCK) ON mig.index_handle = mid.index_handle "
            + "INNER JOIN sys.partitions AS p WITH(NOLOCK) ON p.[object_id] = mid.[object_id] "
            + "INNER JOIN sys.objects AS o WITH(NOLOCK) ON p.[object_id] = o.[object_id] "
            + "WHERE mid.database_id = DB_ID() "
            + "AND p.index_id < 2 "
            + "ORDER BY index_advantage DESC OPTION(RECOMPILE); '";

            ejecutaIndicesSugeridos = listBD + tIndicesSugeridos + whileIni + cIndicesSugeridos //CAMBIAR tABLAS Y CONSULTAS
            + " begin "
            + "exec(@IndicesSugeridos) " //NOMBRE CONSULTA DECLARADA
            + "end "
            + "SET @min = @min + 1 "
            + "end "
            + "select * from  ##IndicesSugeridos "; //NOMBRE TABLA TEMPORAL CONSULTA


            cIndicesFragmentados = 
              "DECLARE @INDICESFRAGMENTADOS NVARCHAR(max) =  'use ' + @db + ' "
            + "' + ' "
            + "insert into ##INDICESFRAGMENTADOS "
            + "SELECT ''Índices Fragmentados'' AS Hoja, "
            + "db_name() as ''BasedeDatos'',		DB_ID() as ''BasedeDatosID'', "
            + "@@SERVERNAME AS Instancia , "
            + "GETDATE() AS FechaHealhCheck, "
            + "DB_NAME(ps.database_id) AS[Database Name],  "
            + "SCHEMA_NAME(o.[schema_id]) AS[Schema Name], "
            + "ps.OBJECT_ID, "
            + "o.[schema_id], "
            + "OBJECT_NAME(ps.OBJECT_ID) AS[Object Name], "
            + "i.[name] AS[Index Name], ps.index_id, "
            + "ps.index_type_desc, CONVERT(decimal(18, 2), ps.avg_fragmentation_in_percent) avg_fragmentation_in_percent , "
            + "ps.fragment_count, "
            + "i.fill_factor, "
            + "i.filter_definition, i.[allow_page_locks] "
            + "FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, N''LIMITED'') AS ps "
            + "INNER JOIN sys.indexes AS i WITH(NOLOCK) ON ps.[object_id] = i.[object_id] AND ps.index_id = i.index_id "
            + "INNER JOIN sys.objects AS o WITH(NOLOCK) ON i.[object_id] = o.[object_id] "
            + "WHERE ps.database_id = DB_ID() "
            + "ORDER BY ps.avg_fragmentation_in_percent DESC OPTION(RECOMPILE) '";
           
            ejecutaIndicesFragmentados = listBD + tIndicesFragmentados + whileIni + cIndicesFragmentados //CAMBIAR tABLAS Y CONSULTAS
            + " begin "
            + "exec(@IndicesFragmentados) " //NOMBRE CONSULTA DECLARADA
            + "end "
            + "SET @min = @min + 1 "
            + "end "
            + "select * from  ##IndicesFragmentados "; //NOMBRE TABLA TEMPORAL CONSULTA

            cIndicesUsados =
              "DECLARE   @INDICESUSADOS NVARCHAR(max) =  'use ' + @db + '  "
            + " ' + ' "
            + "insert into ##INDICESUSADOS "
            + "SELECT ''Indices Usados'' as Hoja, "
            + "db_name() as ''BasedeDatos'', DB_ID() as ''BasedeDatosID'', "
            + "@@SERVERNAME AS Instancia , "
            + "GETDATE() AS FechaHealhCheck, Tab.[object_id], Tab.[schema_id], I.[index_id], I.is_disabled, "
            + "sch.name as esquema, tab.[name] Tabla, "
            + "Ind.[name] AS IndexName, "
            + "SUBSTRING((SELECT '', '' + AC.name "
            + "FROM sys.[tables] AS T "
            + "INNER JOIN sys.[indexes] I "
            + "ON T.[object_id] = I.[object_id] "
            + "INNER JOIN sys.[index_columns] IC "
            + "ON I.[object_id] = IC.[object_id] "
            + "AND I.[index_id] = IC.[index_id] "
            + "INNER JOIN sys.[all_columns] AC "
            + "ON T.[object_id] = AC.[object_id] "
            + "AND IC.[column_id] = AC.[column_id] "
            + "WHERE Ind.[object_id] = I.[object_id] "
            + "AND Ind.index_id = I.index_id "
            + "AND IC.is_included_column = 0 "
            + "ORDER BY IC.key_ordinal "
            + "FOR "
            + "XML PATH('''') "
            + "), 2, 8000) AS KeyCols, "
            + "SUBSTRING((SELECT '', '' + AC.name "
            + "FROM sys.[tables] AS T "
            + "INNER JOIN sys.[indexes] I "
            + "ON T.[object_id] = I.[object_id] "
            + "INNER JOIN sys.[index_columns] IC "
            + "ON I.[object_id] = IC.[object_id] "
            + "AND I.[index_id] = IC.[index_id] "
            + "INNER JOIN sys.[all_columns] AC "
            + "ON T.[object_id] = AC.[object_id] "
            + "AND IC.[column_id] = AC.[column_id] "
            + "WHERE Ind.[object_id] = I.[object_id] "
            + "AND Ind.index_id = I.index_id "
            + "AND IC.is_included_column = 1 "
            + "ORDER BY IC.key_ordinal "
            + "FOR "
            + "XML PATH('''') "
            + "), 2, 8000) AS IncludeCols "
            + ", user_seeks, "
            + "user_scans, user_lookups, user_updates "
            + ", Ind.fill_factor "
            + "FROM sys.[indexes] Ind "
             + "INNER JOIN sys.[tables] AS Tab "
            + "ON Tab.[object_id] = Ind.[object_id] "
            + "INNER JOIN sys.[schemas] AS Sch  "
            + "ON Sch.[schema_id] = Tab.[schema_id] "
            + "JOIN sys.dm_db_index_usage_stats ius ON Ind.index_id = ius.index_id AND Ind.Object_id = ius.object_id  "
            + "join sys.indexes i on ius.index_id = i.index_id and ius.object_id = i.object_id "
            + "where database_id = DB_ID() "
            + "ORDER BY esquema, tabla ' ";

            ejecutaIndicesUsados = listBD + tIndicesUsados + whileIni + cIndicesUsados  //CAMBIAR tABLAS Y CONSULTAS
            + " begin "
            + "exec(@IndicesUsados ) " //NOMBRE CONSULTA DECLARADA
            + "end "
            + "SET @min = @min + 1 "
            + "end "
            + "select * from  ##IndicesUsados  "; //NOMBRE TABLA TEMPORAL CONSULTA


            cDatavsindices = 
              "DECLARE @DatavsIndices NVARCHAR(max) =  'use ' + @db + ' "
            + " ' + ' "
            + "insert into ##DatavsIndices "
            + "SELECT ''Data vs Indices'' as Hoja,  "
            + "GETDATE() AS FechaHealhCheck, "
            + "@@SERVERNAME AS Instancia, DB_ID() as ''BasedeDatosID'', "
            + "db_name() as ''BasedeDatos'', "
            + "a1.object_id, "
            + "a3.schema_id, "
            + "a3.name, "
            + "a2.name AS[Tablename], "
            + "a1.rows as Row_Cnt, "
            + "(a1.reserved + ISNULL(a4.reserved, 0)) * 8 AS Reserved, "
            + "a1.data * 8 AS Data, "
            + "(CASE WHEN(a1.used + ISNULL(a4.used, 0)) > a1.data THEN(a1.used + ISNULL(a4.used, 0)) - a1.data ELSE 0 END) *8 AS Index_Size "
            + "FROM "
            + "(SELECT ps.object_id, "
            + "SUM(CASE  WHEN(ps.index_id < 2) THEN row_count "
            + "ELSE 0 "
            + "END "
            + ") AS[rows], "
            + "SUM(ps.reserved_page_count) AS Reserved, "
            + "SUM( "
            + "CASE "
            + "WHEN(ps.index_id < 2) THEN(ps.in_row_data_page_count + ps.lob_used_page_count + ps.row_overflow_used_page_count) "
            + "ELSE(ps.lob_used_page_count + ps.row_overflow_used_page_count) "
            + "END "
            + ") AS Data, "
            + "SUM(ps.used_page_count) AS used "
            + "FROM sys.dm_db_partition_stats ps "
            + "WHERE ps.object_id NOT IN(SELECt - 9999999 FROM sys.tables) "
            + "GROUP BY ps.object_id) AS a1 "
            + "LEFT OUTER JOIN "
            + "(SELECT "
            + "it.parent_id, "
            + "SUM(ps.reserved_page_count) AS Reserved, "
            + "SUM(ps.used_page_count) AS used "
            + "FROM sys.dm_db_partition_stats ps "
            + "INNER JOIN sys.internal_tables it ON(it.object_id = ps.object_id) "
            + "WHERE it.internal_type IN(202, 204) "
            + "GROUP BY it.parent_id) AS a4 ON(a4.parent_id = a1.object_id) "
            + "INNER JOIN sys.all_objects a2  ON(a1.object_id = a2.object_id) "
            + "INNER JOIN sys.schemas a3 ON(a2.schema_id = a3.schema_id) "
            + "WHERE a2.type<> N''S'' and a2.type<> N''IT'' "
            + "order by row_cnt desc '";

            ejecutaDatavsindices = listBD + tDatavsIndices + whileIni + cDatavsindices  //CAMBIAR tABLAS Y CONSULTAS
            + " begin "
            + "exec(@DatavsIndices ) " //NOMBRE CONSULTA DECLARADA
            + "end "
            + "SET @min = @min + 1 "
            + "end "
            + "select * from  ##DatavsIndices  "; //NOMBRE TABLA TEMPORAL CONSULTA



        }
       

        //Declaraciones
        //Declaraciones Health Check_Instancia
        public string propiedadesInstancia;
        public string PropiedadesServidor;
        public string servicesInfo;
        public string contadores;
        public string infoHardware;
        public string propiedadesBD;
        public string autoCrecimiento;
        public string UsoCPUBD;
        public string UsoDiscoBD;
        public string estadisticasBD;
        public string waits;
        public string sysadmin;
        //Declaraciones Health Check Base de Datos
        public string tCantidadEjecucionesSP;
        public string tIndicesSugeridos;
        public string tIndicesFragmentados;
        public string tIndicesUsados;
        public string tDatavsIndices;
        public string listBD;
        public string ejecutaCantidaddeEjecuciones;
        public string whileIni;
        public string cCantidadEjecucionesSP;
        public string cIndicesSugeridos;
        public string ejecutaIndicesSugeridos;
        public string cIndicesFragmentados;
        public string cIndicesUsados;
        public string ejecutaIndicesFragmentados;
        public string ejecutaIndicesUsados;
        public string cDatavsindices;
        public string ejecutaDatavsindices;


    }
}
