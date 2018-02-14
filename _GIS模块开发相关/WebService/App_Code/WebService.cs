using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Oracle.DataAccess.Client;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

	// 插入工程信息, 支持批量插入
	[WebMethod]
	public string InsertProject(string strParam)
	{
		JObject result = new JObject();
		JArray projects = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;

				for (int i = 0; i < projects.Count; i++) {
					JObject proj = (JObject)projects[i];
					SqlBuilder sb = new SqlBuilder();
					sb.AddField("ID", (int)proj.GetValue("id"));
					sb.AddField("NAME", (string)proj.GetValue("name"));
					cmd.CommandText = sb.BuildInsertSQL("TBL_PROJECT");
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}

	// 根据工程ID, 读取工程信息
	[WebMethod]
	public string ReadProject(string strParam)
	{
		JObject result = new JObject();
		JArray ids = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			JArray rows = new JArray();
			result.Add("rows", rows);
			OracleCommand cmd = conn.CreateCommand();
			for (int i = 0; i < ids.Count; i++) {
				cmd.CommandText = string.Format("SELECT * FROM TBL_PROJECT WHERE \"ID\"={0}", (int)ids[i]);
				JObject project = new JObject();
				rows.Add(project);
				OracleDataReader reader = cmd.ExecuteReader();
				if (reader.Read()) {
					// context.Response.Write(reader[1]);
					// context.Response.Write("\n");
					project.Add("id", (int)reader["ID"]);
					project.Add("name", (string)reader["NAME"]);
				}
				reader.Close();
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}

	// 更新工程信息
	[WebMethod]
	public string UpdateProject(string strParam)
	{
		JObject result = new JObject();
		JArray projects = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;

				for (int i = 0; i < projects.Count; i++) {
					JObject proj = (JObject)projects[i];
					SqlBuilder sb = new SqlBuilder();	// 结构与 InsertProject 相同, 但根据ID来更新其他字段, ID不变
					JToken token;
					if (proj.TryGetValue("name", out token))
						sb.AddField("NAME", (string)token);
					cmd.CommandText = sb.BuildUpdateSQL("TBL_PROJECT", "ID", proj.GetValue("id").ToString());
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}	
	
	// 删除工程
	[WebMethod]
	public string DeleteProject(string strParam)
	{
		JObject result = new JObject();
		JArray ids = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;
				cmd.CommandText = string.Format("DELETE FROM TBL_PROJECT WHERE \"ID\" IN ({0})", string.Join(",", ids));
				cmd.ExecuteNonQuery();
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 查询工程列表
	[WebMethod]
	public string QueryProject()
	{
		JObject result = new JObject();
		JArray rows = new JArray();
		result.Add("rows", rows);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT * FROM TBL_PROJECT";
			OracleDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				JObject project = new JObject();
				project.Add("id", (int)reader["ID"]);
				project.Add("name", (string)reader["NAME"]);
				rows.Add(project);
			}
			reader.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 插入管井信息, 支持批量插入
	[WebMethod]
	public string InsertWell(string strParam)
	{
		JObject result = new JObject();
		JArray wells = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;

				for (int i = 0; i < wells.Count; i++) {
					JObject well = (JObject)wells[i];
					SqlBuilder sb = new SqlBuilder();
					sb.AddField("OBJECTID", (int)well.GetValue("id"));
					sb.AddField("NAME", (string)well.GetValue("name"));
					sb.AddField("LOCATION", (string)well.GetValue("location"));
					sb.AddField("PROJECTID", (int)well.GetValue("projectId"));
					JArray coords = (JArray)well.GetValue("coords");
					sb.AddField("SHAPE", string.Format("SDE.ST_Point({0}, {1}, 2)", 
						(double)coords[0], (double)coords[1]), false);
					cmd.CommandText = sb.BuildInsertSQL("TBL_WELL");
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}

	// 根据管井ID, 读取管井信息
	[WebMethod]
	public string ReadWell(string strParam)
	{
		JObject result = new JObject();
		JArray ids = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			JArray rows = new JArray();
			result.Add("rows", rows);
			OracleCommand cmd = conn.CreateCommand();
			for (int i = 0; i < ids.Count; i++) {
				cmd.CommandText = string.Format("SELECT OBJECTID,NAME,LOCATION,PROJECTID,SDE.ST_AsText(SHAPE) SHAPE FROM TBL_WELL WHERE OBJECTID={0}", (int)ids[i]);
				JObject well = new JObject();
				rows.Add(well);
				OracleDataReader reader = cmd.ExecuteReader();
				if (reader.Read()) {
					// context.Response.Write(reader[1]);
					// context.Response.Write("\n");
					well.Add("id", (int)(decimal)reader["OBJECTID"]);
					well.Add("projectId", (int)(decimal)reader["PROJECTID"]);
					well.Add("name", (string)reader["NAME"]);
					well.Add("location", (string)reader["LOCATION"]);
					string shape = (string)reader["SHAPE"];
					shape = shape.Substring(9);
					shape = shape.Substring(0, shape.Length - 1);
					string[] ss = shape.Split(new char[] { ' ' });
					JArray coords = new JArray();
					coords.Add(decimal.Parse(ss[0].Trim()));
					coords.Add(decimal.Parse(ss[1].Trim()));
					well.Add("coords", coords);
				}
				reader.Close();
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}

	// 更新管井信息
	[WebMethod]
	public string UpdateWell(string strParam)
	{
		JObject result = new JObject();
		JArray projects = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;

				for (int i = 0; i < projects.Count; i++) {
					JObject proj = (JObject)projects[i];
					SqlBuilder sb = new SqlBuilder();
					// sb.AddField("ID", (string)proj.GetValue("id"));
					JToken token;
					if (proj.TryGetValue("name", out token))
						sb.AddField("NAME", (string)token);
					if (proj.TryGetValue("location", out token))
						sb.AddField("LOCATION", (string)token);
					if (proj.TryGetValue("projectId", out token)) 
						sb.AddField("PROJECTID", (int)token);
					if (proj.TryGetValue("coord", out token)) {
						JArray coord = (JArray)token;
						sb.AddField("SHAPE", string.Format("SDE.ST_Point({0}, {1}, 2)",
							(double)coord[0], (double)coord[1]), false);
					}
					cmd.CommandText = sb.BuildUpdateSQL("TBL_WELL", "OBJECTID", proj.GetValue("id").ToString());
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 删除管井
	[WebMethod]
	public string DeleteWell(string strParam)
	{
		JObject result = new JObject();
		JArray ids = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;
				cmd.CommandText = string.Format("DELETE FROM TBL_WELL WHERE OBJECTID IN ({0})", string.Join(",", ids));
				cmd.ExecuteNonQuery();
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 查询管井列表
	[WebMethod]
	public string QueryWell()
	{
		JObject result = new JObject();
		JArray rows = new JArray();
		result.Add("rows", rows);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT OBJECTID,NAME,LOCATION,PROJECTID,SDE.ST_AsText(SHAPE) SHAPE FROM TBL_WELL";
			OracleDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				JObject well = new JObject();
				well.Add("id", (int)(decimal)reader["OBJECTID"]);
				well.Add("projectId", (int)(decimal)reader["PROJECTID"]);
				well.Add("name", (string)reader["NAME"]);
				well.Add("location", (string)reader["LOCATION"]);
				string shape = (string)reader["SHAPE"];
				shape = shape.Substring(9);
				shape = shape.Substring(0, shape.Length - 1);
				string[] ss = shape.Split(new char[] { ' ' });
				JArray coords = new JArray();
				coords.Add(decimal.Parse(ss[0].Trim()));
				coords.Add(decimal.Parse(ss[1].Trim()));
				well.Add("coords", coords);
				rows.Add(well);
			}
			reader.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 插入管线信息, 支持批量插入
	[WebMethod]
	public string InsertPipe(string strParam)
	{
		JObject result = new JObject();
		JArray projects = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;

				for (int i = 0; i < projects.Count; i++) {
					JObject proj = (JObject)projects[i];
					SqlBuilder sb = new SqlBuilder();
					sb.AddField("ID", (int)proj.GetValue("id"));
					sb.AddField("PROJECTID", (int)proj.GetValue("projectId")); 
					sb.AddField("NAME", (string)proj.GetValue("name"));
					sb.AddField("LOCATION", (string)proj.GetValue("location"));
					sb.AddField("STARTWELLID", (int)proj.GetValue("startWellId"));
					sb.AddField("ENDWELLID", (int)proj.GetValue("endWellId"));
					cmd.CommandText = sb.BuildInsertSQL("TBL_PIPE");
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 根据管线ID, 读取管线信息
	[WebMethod]
	public string ReadPipe(string strParam)
	{
		JObject result = new JObject();
		JArray ids = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			JArray rows = new JArray();
			result.Add("rows", rows);
			OracleCommand cmd = conn.CreateCommand();
			for (int i = 0; i < ids.Count; i++) {
				cmd.CommandText = string.Format("SELECT * FROM TBL_PIPE WHERE \"ID\"={0}", (int)ids[i]);
				JObject project = new JObject();
				rows.Add(project);
				OracleDataReader reader = cmd.ExecuteReader();
				if (reader.Read()) {
					// context.Response.Write(reader[1]);
					// context.Response.Write("\n");
					project.Add("id", (int)reader["ID"]);
					project.Add("PROJECTID", (int)reader["PROJECTID"]);
					project.Add("name", (string)reader["NAME"]);
					project.Add("location", (string)reader["LOCATION"]);
					project.Add("startWellId", (int)reader["STARTWELLID"]);
					project.Add("endWellId", (int)reader["ENDWELLID"]);
				}
				reader.Close();
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 更新管线信息
	[WebMethod]
	public string UpdatePipe(string strParam)
	{
		JObject result = new JObject();
		JArray projects = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;

				for (int i = 0; i < projects.Count; i++) {
					JObject proj = (JObject)projects[i];
					SqlBuilder sb = new SqlBuilder();
					JToken token;
					if (proj.TryGetValue("projectId", out token))
						sb.AddField("PROJECTID", (int)token); 
					if (proj.TryGetValue("name", out token))
						sb.AddField("NAME", (string)token);
					if (proj.TryGetValue("location", out token))
						sb.AddField("LOCATION", (string)token);
					if (proj.TryGetValue("startWellId", out token)) 
						sb.AddField("STARTWELLID", (int)token);
					if (proj.TryGetValue("endWellId", out token)) 
						sb.AddField("ENDWELLID", (int)token);
					cmd.CommandText = sb.BuildUpdateSQL("TBL_PIPE", "ID", proj.GetValue("id").ToString());
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
	
	// 删除管线信息
	[WebMethod]
	public string DeletePipe(string strParam)
	{
		JObject result = new JObject();
		JArray ids = JArray.Parse(strParam);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleTransaction transaction = null;
			try {
				transaction = conn.BeginTransaction();
				OracleCommand cmd = conn.CreateCommand();
				cmd.Connection = conn;
				cmd.Transaction = transaction;
				cmd.CommandText = string.Format("DELETE FROM TBL_PIPE WHERE \"ID\" IN ({0})", string.Join(",", ids));
				cmd.ExecuteNonQuery();
				transaction.Commit();
				result.Add("error", 0);
			}
			catch (Exception ex) {
				if (transaction != null) {
					transaction.Rollback();
				}
				result.Add("error", 1);
				result.Add("message", ex.Message);
			}
			conn.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}

	// 查询管线列表
	[WebMethod]
	public string QueryPipe()
	{
		JObject result = new JObject();
		JArray rows = new JArray();
		result.Add("rows", rows);
		using (OracleConnection conn = Common.GetSqlConnection()) {
			OracleCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT * FROM TBL_PIPE";
			OracleDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				JObject project = new JObject();
				project.Add("id", (int)reader["ID"]);
				project.Add("PROJECTID", (int)reader["PROJECTID"]);
				project.Add("name", (string)reader["NAME"]);
				project.Add("location", (string)reader["LOCATION"]);
				project.Add("startWellId", (int)reader["STARTWELLID"]);
				project.Add("endWellId", (int)reader["ENDWELLID"]);
				rows.Add(project);
			}
			reader.Close();
		}
		return result.ToString(Newtonsoft.Json.Formatting.Indented, null);
	}
}
