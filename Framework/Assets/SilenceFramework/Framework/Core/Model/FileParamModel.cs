using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 网络传输中的Post文件类型模型
/// </summary>
public class FileParamModel
{
    /// <summary>
    /// 字段名
    /// </summary>
    public string FieldName { get; set; }
    /// <summary>
    /// 内容字节数组
    /// </summary>
    public byte[] Content { get; set; }
    /// <summary>
    /// 文件名,必须带上扩展名 [可选]
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// MimeType 类型 [可选]  例： image/jpeg , image/png 
    /// </summary>
    public string MimeType { get; set; }

    public FileParamModel(string fieldName, string filePath)
    {
        this.FieldName = fieldName;
        this.FileName = Path.GetFileName(filePath);
        this.Content = File.ReadAllBytes(filePath);
    }
}
