using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using System.Data.SqlClient;
// using System.Data.SqlClient;

public class SqlBuilder
{
	public struct fvalue
	{
		public string strFieldName;
		public string strValue;
	}

	private List<fvalue> fvalues = new List<fvalue>();

	public SqlBuilder()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public void AddField(string strFieldName)
	{
		fvalue fv;
		fv.strFieldName = strFieldName;
		fv.strValue = "NULL";
		fvalues.Add(fv);
	}

	public void AddField(string strFieldName, string strFieldValue)
	{
		fvalue fv;
		strFieldValue = strFieldValue.Trim();
		fv.strFieldName = strFieldName;
		fv.strValue = "'" + strFieldValue.Replace("'", "''") + "'";
		fvalues.Add(fv);
	}

	public void AddField(string strFieldName, string strFieldValue, bool bQuote)
	{
		fvalue fv;
		strFieldValue = strFieldValue.Trim();
		fv.strFieldName = strFieldName;
		if (bQuote)
			fv.strValue = "'" + strFieldValue.Replace("'", "''") + "'";
		else
			fv.strValue = strFieldValue;
		fvalues.Add(fv);
	}

	public void AddField(string strFieldName, double lfFieldValue)
	{
		fvalue fv;
		fv.strFieldName = strFieldName;
		fv.strValue = lfFieldValue.ToString();
		fvalues.Add(fv);
	}

	public void AddField(string strFieldName, int nFieldValue)
	{
		fvalue fv;
		fv.strFieldName = strFieldName;
		fv.strValue = nFieldValue.ToString();
		fvalues.Add(fv);
	}

	public void AddField(string strFieldName, decimal nFieldValue)
	{
		fvalue fv;
		fv.strFieldName = strFieldName;
		fv.strValue = nFieldValue.ToString();
		fvalues.Add(fv);
	}

	public string BuildInsertSQL(string strTableName, bool bReset)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("INSERT INTO \"");
		sb.Append(strTableName);
		sb.Append("\"");
		int num = 0;
		foreach (fvalue fv in fvalues) {
			if (num == 0)
				sb.Append("(\"");
			else
				sb.Append(",\"");
			sb.Append(fv.strFieldName);
			sb.Append("\"");
			num++;
		}
		sb.Append(") VALUES");
		num = 0;
		foreach (fvalue fv in fvalues) {
			if (num == 0)
				sb.Append("(");
			else
				sb.Append(",");
			sb.Append(fv.strValue);
			num++;
		}
		sb.Append(")");
		if (bReset)
			Reset();
		return sb.ToString();
	}

	public string BuildUpdateSQL(string strTableName, bool bReset)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("UPDATE \"");
		sb.Append(strTableName);
		sb.Append("\" SET");
		int num = 0;
		foreach (fvalue fv in fvalues) {
			if (num == 0)
				sb.Append(" \"");
			else
				sb.Append(",\"");
			sb.Append(fv.strFieldName);
			sb.Append("\"=");
			sb.Append(fv.strValue);
			num++;
		}
		if (bReset)
			Reset();
		return sb.ToString();
	}

	public string BuildUpdateSQL(string strTableName, string s1, string s2, bool bReset)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("UPDATE \"");
		sb.Append(strTableName);
		sb.Append("\" SET");
		int num = 0;
		foreach (fvalue fv in fvalues) {
			if (num == 0)
				sb.Append(" \"");
			else
				sb.Append(",\"");
			sb.Append(fv.strFieldName);
			sb.Append("\"=");
			sb.Append(fv.strValue);
			num++;
		}
		sb.AppendFormat(" WHERE \"{0}\"={1}", s1, s2);
		if (bReset)
			Reset();
		return sb.ToString();
	}

	public void Reset()
	{
		fvalues.Clear();
	}

	public string BuildInsertSQL(string strTableName)
	{
		return BuildInsertSQL(strTableName, true);
	}

	public string BuildUpdateSQL(string strTableName)
	{
		return BuildUpdateSQL(strTableName, true);
	}

	public string BuildUpdateSQL(string strTableName, string s1, string s2)
	{
		return BuildUpdateSQL(strTableName, s1, s2, true);
	}

#if false
	public string BuildDuplicateSQL(SqlCommand cmd, string strTableName)
	{
		return BuildDuplicateSQL(cmd, strTableName, string.Empty, string.Empty);
	}

	public string BuildDuplicateSQL(SqlCommand cmd, string strTableName, string s1, string s2)
	{
		cmd.CommandText = string.Format("SELECT * FROM [{0}]", strTableName);
		SqlDataReader reader = cmd.ExecuteReader();
		// StringBuilder sb = new StringBuilder();
		StringBuilder fields = new StringBuilder();
		StringBuilder values = new StringBuilder();
		// sb.AppendFormat("INSERT INTO [{0}](", strTableName);
		// int nFieldCount = 0;
		for (int i = 0; i < reader.FieldCount; i++) {
			string fname = reader.GetName(i);
			string value = string.Empty;
			for (int m = 0; m < fvalues.Count; m++) {
				if (fvalues[m].strFieldName == fname) {
					value = fvalues[m].strValue;
					break;
				}
			}
			if (value.Length > 0) {
				if (value != "NULL") {
					fields.AppendFormat(",[{0}]", fname);
					values.AppendFormat(",{0}", value);
				}
			}
			else {
				fields.AppendFormat(",[{0}]", fname);
				values.AppendFormat(",[{0}]", fname);
			}
		}
		reader.Close();

		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("INSERT INTO [{0}](", strTableName);
		sb.Append(fields.ToString().Substring(1));
		sb.Append(") SELECT ");
		sb.Append(values.ToString().Substring(1));
		sb.AppendFormat(" FROM [{0}]", strTableName);

		if (s1.Length > 0)
			sb.AppendFormat(" WHERE [{0}]={1}", s1, s2);

		return sb.ToString();
	}
#endif
}
