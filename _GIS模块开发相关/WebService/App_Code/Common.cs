using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
	public Common()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public static OracleConnection GetSqlConnection()
	{
		ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["connstr"];
		OracleConnection conn = new OracleConnection(settings.ConnectionString);
		conn.Open();
		return conn;
	}
}