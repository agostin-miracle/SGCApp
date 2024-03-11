using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace SCGApp
{
    public class GenWord
    {

        /// <summary>
        /// Imagem de Capa
        /// </summary>
        public static string ImagePath { get; set; } = @"C:\Users\agostinho.milagres\Downloads";

        public static string ImageHeader { get; set; } = "petcare_docx.png";
        /// <summary>
        /// Caminho de Gravação
        /// </summary>
        public static string PathOut { get; set; } = "C:\\LOGS";
        /// <summary>
        /// Titulo da Capa
        /// </summary>
        public static string HeaderTitle { get; set; } = "Modelo Físico DataLake";
        public static string SubTitle { get; set; } = "";

        /// <summary>
        /// Nome do Arquivo de Saida
        /// </summary>
        public static string OutPutFile { get; set; } = "Modelo_Fisico_CEP.docx";

        /*
         * 
         * 
         * 
         * 
         * 
         *     HeaderTitle = "DW";
            SubTitle = "Procedures e Funções";
            OutPutFile = "MOVIDESK_PROCEDURES.docx";
            string caminho = @"C:\Repositorio\PetCareDW.MoviDesk\Business\DbScripts\Docs";
            GenerateProceduresDoc(caminho);

            OutPutFile = "MOVIDESK_Modelo_Fisico.docx";
            SubTitle = "Tabelas e Views";
            GenerateTablesDoc(caminho);
         * 
         * */


        public static void GenerateTablesDoc(string path, string outputfile, string headertitle = "", string subtitle = "Tabelas e Views")
        {

            HeaderTitle = headertitle;
            SubTitle = subtitle;
            if (String.IsNullOrWhiteSpace(outputfile))
                OutPutFile = headertitle + "_MODELO_FISICO";
            else
                OutPutFile = outputfile;

            string file = Path.Combine(path, "tables.xml");
            XmlDocument doc = new XmlDocument();

            doc.Load(file);
            List<TablesModel> result = new List<TablesModel>();
            XmlNodeList node = doc.SelectNodes("//root/table");
            for (int i = 0; i < node.Count; i++)
            {
                string tbname = node[i].SelectSingleNode("@id").Value.Trim();
                string tbdesc = node[i].SelectSingleNode("@td").Value.Trim();
                string comments = "";

                if (node[i].SelectSingleNode("@comments") != null)
                    comments = node[i].SelectSingleNode("@comments").Value.Trim();
                string tbtype = "TABLE";
                if (node[i].SelectSingleNode("@type") != null)
                {
                    tbtype = node[i].SelectSingleNode("@type").Value.Trim();
                }
                TablesModel item = new TablesModel(tbname, tbdesc, comments);
                item.Type = tbtype;
                var columns = node[i].SelectNodes("item");
                for (int j = 0; j < columns.Count; j++)
                {
                    ColumnsModel column = new ColumnsModel();
                    column.Order = columns[j].SelectSingleNode("@co").Value.Trim();
                    column.Name = columns[j].SelectSingleNode("@cn").Value.Trim();
                    column.Description = columns[j].SelectSingleNode("@cd").Value.Trim();
                    column.DataType = columns[j].SelectSingleNode("@ct").Value.Trim();
                    column.Size = columns[j].SelectSingleNode("@cs").Value.Trim();
                    column.Comments = columns[j].SelectSingleNode("@cc").Value.Trim();
                    if (columns[j].SelectSingleNode("@ss") != null)
                        column.System = columns[j].SelectSingleNode("@ss").Value.Trim();
                    if (columns[j].SelectSingleNode("@fm") != null)
                        column.Format = columns[j].SelectSingleNode("@fm").Value.Trim();
                    column.IsIdentity = false;
                    column.IsKey = false;
                    column.IsNullable = false;
                    item.Columns.Add(column);
                }

                var index = node[i].SelectNodes("indexes/index");
                for (int j = 0; j < index.Count; j++)
                {
                    IndexModel column = new IndexModel();
                    column.Name = index[j].SelectSingleNode("@nm").Value.Trim();
                    column.ColumnName = index[j].SelectSingleNode("@cn").Value.Trim();
                    column.Type = index[j].SelectSingleNode("@ti").Value.Trim();
                    column.IsPrimaryKey = index[j].SelectSingleNode("@pk").Value.Trim() == "1";
                    column.IsUnique = index[j].SelectSingleNode("@un").Value.Trim() == "1";
                    column.IsUniqueConstraint = index[j].SelectSingleNode("@uc").Value.Trim() == "1";
                    item.Indexes.Add(column);
                }

                var fk = node[i].SelectNodes("foreignkeys/index");
                if (fk.Count > 0)
                {
                    for (int j = 0; j < fk.Count; j++)
                    {
                        ForeignKeyModel column = new ForeignKeyModel();
                        column.Name = fk[j].SelectSingleNode("@nm").Value.Trim();
                        column.ParentTable = fk[j].SelectSingleNode("@parenttable").Value.Trim();
                        column.ParentColumn = fk[j].SelectSingleNode("@parentcolumn").Value.Trim();
                        column.ReferencedTable = fk[j].SelectSingleNode("@referencedtable").Value.Trim();
                        column.ReferencedColumn = fk[j].SelectSingleNode("@referencedcolumn").Value.Trim();

                        item.ForeignKeys.Add(column);
                    }
                }

                result.Add(item);
            }

            if (result.Count > 0)
            {
                /*
              *  CAPA DO DOCUMENTO
              */
                string _outfile = Path.Combine(PathOut, OutPutFile);
                if (File.Exists(_outfile))
                    File.Delete(_outfile);

                using (var document = DocX.Create(_outfile))
                {
                    document.AddHeaders();
                    document.AddFooters();
                    document.DifferentFirstPage = true;
                    document.DifferentOddAndEvenPages = true;

                    // Add a simple image from disk.
                    var image = document.AddImage(Path.Combine(ImagePath, ImageHeader));

                    // Set Picture Height and Width.
                    var picture = image.CreatePicture(250, 250);

                    var capa = document.InsertParagraph("").SpacingBefore(200d);
                    capa.Alignment = Alignment.center;
                    capa.AppendPicture(picture);
                    //capa.InsertPicture(picture);
                    //picture.HorizontalAlignment = PictureHorizontalAlignment.CenteredRelativeToPage;
                    //// Set vertical alignement with an offset from top of paragraph.
                    //picture.VerticalOffsetAlignmentFrom = PictureVerticalOffsetAlignmentFrom.Paragraph;
                    //picture.VerticalOffset = 25d;
                    // Insert Picture in paragraph.
                    var t1 = document.InsertParagraph("Mapeamento de Objetos").SpacingAfter(20d).FontSize(14d).Font(new Xceed.Document.NET.Font("Calibri")).Bold();
                    t1.Color(System.Drawing.ColorTranslator.FromHtml("#006790"));
                    t1.Alignment = Alignment.center;


                    t1 = document.InsertParagraph(SubTitle).SpacingAfter(20d).FontSize(13d).Font(new Xceed.Document.NET.Font("Calibri")).Bold();
                    t1.Color(System.Drawing.ColorTranslator.FromHtml("#006790"));
                    t1.Alignment = Alignment.center;

                    var t2 = document.InsertParagraph(HeaderTitle + " em " + DateTime.Now.ToString("dd-MM-yyyy")).SpacingAfter(20d).FontSize(12d).Font(new Xceed.Document.NET.Font("Calibri")).Bold();
                    t2.Color(System.Drawing.ColorTranslator.FromHtml("#006790"));
                    t2.Alignment = Alignment.center;
                    t2.InsertPageBreakAfterSelf();

                    //document.Sections[0].AddHeaders();
                    //document.Sections[0].AddFooters();
                    ////document.AddHeaders();
                    ////document.AddFooters();
                    document.DifferentFirstPage = false;
                    document.DifferentOddAndEvenPages = true;


                    document.InsertParagraph("ÍNDICE").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;
                    var tocSwitches = new Dictionary<TableOfContentsSwitches, string>()
        {
          { TableOfContentsSwitches.O, "1-3"},
          { TableOfContentsSwitches.U, ""},
          { TableOfContentsSwitches.Z, ""},
          { TableOfContentsSwitches.H, ""},
        };
                    document.InsertTableOfContents("Objetos de Dados", tocSwitches);


                    document.InsertParagraph().InsertPageBreakAfterSelf();

                    document.InsertSectionPageBreak();
                    //document.DifferentFirstPage = true;

                    document.Sections[1].AddHeaders();
                    document.Sections[1].AddFooters();


                    var headers1 = document.Sections[1].Headers;
                    headers1.First.InsertParagraph(HeaderTitle);
                    headers1.Even.InsertParagraph(HeaderTitle);
                    headers1.Odd.InsertParagraph(HeaderTitle);


                    var footers1 = document.Sections[1].Footers;
                    footers1.First.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.right;

                    footers1.Even.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.right;

                    footers1.Odd.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.right;



                    var h = document.InsertParagraph();
                    h.Append("Tabelas/Views");
                    h.StyleId = "Heading1";
                    foreach (TablesModel tb in result.OrderBy(x => x.Type).OrderBy(y => y.Name))
                    {

                        var p = document.InsertParagraph();
                        p.Append(tb.Name);
                        p.StyleId = "Heading2";

                        p = document.InsertParagraph();
                        p.Append($"     Tipo:{tb.Type}").Font(new Xceed.Document.NET.Font("Arial")).Bold();
                        p.StyleId = "Normal";

                        p = document.InsertParagraph();
                        p.Append($"    Alias:{tb.Description}").Font(new Xceed.Document.NET.Font("Arial")).Bold();
                        p.StyleId = "Normal";

                        if (!String.IsNullOrWhiteSpace(tb.Comments))
                        {
                            p = document.InsertParagraph();
                            p.Append($"Propósito:{tb.Comments.Replace("@",Environment.NewLine)}").Font(new Xceed.Document.NET.Font("Arial")).Bold();
                            p.StyleId = "Normal";
                        }
                        //p = document.InsertParagraph();
                        //p.Alignment = Alignment.left;
                        //p.Append(tb.Description)
                        //.Font(new Xceed.Document.NET.Font("Arial"))
                        //.Color(Color.Blue)
                        //.Bold()
                        //.Append(": ").Append(tb.Comments).Color(Color.Black).SpacingAfter(10);
                        //p.StyleId = "Normal";



                        var t = document.AddTable(tb.Columns.Count + 1, 6);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.LightGridAccent1;
                        t.AutoFit = AutoFit.ColumnWidth;



                        t.SetWidths(new float[] { 80, 170, 140, 80, 110, 400 }, true);


                        t.Rows[0].Cells[0].Paragraphs[0].Append("#");
                        t.Rows[0].Cells[1].Paragraphs[0].Append("Campo");
                        t.Rows[0].Cells[2].Paragraphs[0].Append("Tipo");
                        t.Rows[0].Cells[3].Paragraphs[0].Append("Nulo");
                        t.Rows[0].Cells[4].Paragraphs[0].Append("Formato");
                        t.Rows[0].Cells[5].Paragraphs[0].Append("Descrição");
                        int i = 1;
                        foreach (var item in tb.Columns)
                        {
                            t.Rows[i].Cells[0].Paragraphs[0].Append(item.Order.ToString()).FontSize(11d);
                            t.Rows[i].Cells[1].Paragraphs[0].Append(item.Name.Trim()).FontSize(11d);

                            if (!(String.IsNullOrEmpty(item.Size) || string.IsNullOrWhiteSpace(item.Size)))
                            {
                                t.Rows[i].Cells[2].Paragraphs[0].Append(item.DataType + "(" + item.Size + ")").FontSize(11d);
                            }
                            else
                                t.Rows[i].Cells[2].Paragraphs[0].Append(item.DataType).FontSize(11d);
                            t.Rows[i].Cells[3].Paragraphs[0].Append(item.IsNullable == true ? "Sim" : "Não").FontSize(11d);

                            if (item.DataType.ToLower() == "tinyint")
                            {
                                t.Rows[i].Cells[4].Paragraphs[0].Append("999").FontSize(11d);
                            }
                            else
                                t.Rows[i].Cells[4].Paragraphs[0].Append(item.Format).FontSize(11d);

                            t.Rows[i].Cells[5].Paragraphs[0].Append(item.Description).FontSize(11d);

                            i++;
                        }



                        document.InsertTable(t);

                        byte first = 0;
                        foreach (var item in tb.Columns)
                        {
                            if (!String.IsNullOrWhiteSpace(item.Comments))
                            {
                                if (first == 0)
                                {
                                    p = document.InsertParagraph();
                                    p.Append("Observações");
                                    p.StyleId = "Heading4";
                                    first = 1;
                                }
                                string[] lines = item.Comments.Split('@');
                                for (int u = 0; u < lines.Length; u++)
                                {
                                    p = document.InsertParagraph();
                                    if (u == 0)
                                    {
                                        p.Spacing(10).Append(item.Order.ToString()).Bold().Append(":" + lines[u]).SpacingLine(12);
                                    }
                                    else
                                        p.Spacing(10).Append(lines[u]).SpacingLine(12);

                                    p.StyleId = "Normal";
                                }


                            }
                        }

                        if (tb.Indexes.Count > 0)
                        {
                            var idx = document.AddTable(tb.Indexes.Count + 1, 7);
                            idx.Alignment = Alignment.center;
                            idx.Design = TableDesign.LightGridAccent1;

                            idx.SetWidthsPercentage(new[] { 80f, 80f, 70f, 80f, 80f, 70f }, 490);
                            idx.Rows[0].Cells[0].Paragraphs[0].Append("Índice");
                            idx.Rows[0].Cells[1].Paragraphs[0].Append("Tipo");
                            idx.Rows[0].Cells[2].Paragraphs[0].Append("Campo");
                            idx.Rows[0].Cells[3].Paragraphs[0].Append("PK");
                            idx.Rows[0].Cells[4].Paragraphs[0].Append("Unique");
                            idx.Rows[0].Cells[5].Paragraphs[0].Append("Unique Constraint");


                            i = 1;

                            foreach (var item in tb.Indexes)
                            {
                                idx.Rows[i].Cells[0].Paragraphs[0].Append(item.Name).FontSize(11d);
                                idx.Rows[i].Cells[1].Paragraphs[0].Append(item.Type);
                                idx.Rows[i].Cells[2].Paragraphs[0].Append(item.ColumnName);
                                idx.Rows[i].Cells[3].Paragraphs[0].Append(item.IsPrimaryKey == true ? "Sim" : "Não").Bold();
                                idx.Rows[i].Cells[4].Paragraphs[0].Append(item.IsUnique == true ? "Sim" : "Não");
                                idx.Rows[i].Cells[5].Paragraphs[0].Append(item.IsUniqueConstraint == true ? "Sim" : "Não");

                                i++;

                            }


                            p = document.InsertParagraph("Índices da Tabela").FontSize(15d).SpacingAfter(15d);
                            p.StyleId = "Heading3";


                            // Insert the Table after the Paragraph.
                            p.InsertTableAfterSelf(idx);
                        }


                        if (tb.ForeignKeys.Count > 0)
                        {
                            var fk = document.AddTable(tb.ForeignKeys.Count + 1, 5);
                            fk.Alignment = Alignment.center;


                            fk.Design = TableDesign.LightGridAccent1;

                            //fk.AutoFit = AutoFit.ColumnWidth;
                            fk.SetWidthsPercentage(new[] { 120f, 80f, 80f, 80f, 90f }, 450);
                            fk.Rows[0].Cells[0].Paragraphs[0].Append("Índice");
                            fk.Rows[0].Cells[1].Paragraphs[0].Append("Tabela Pai");
                            fk.Rows[0].Cells[2].Paragraphs[0].Append("Coluna Pai");
                            fk.Rows[0].Cells[3].Paragraphs[0].Append("Tabela Referência");
                            fk.Rows[0].Cells[4].Paragraphs[0].Append("Coluna Referência");


                            i = 1;
                            foreach (var item in tb.ForeignKeys)
                            {
                                fk.Rows[i].Cells[0].Paragraphs[0].Append(item.Name);
                                fk.Rows[i].Cells[1].Paragraphs[0].Append(item.ParentTable);
                                fk.Rows[i].Cells[2].Paragraphs[0].Append(item.ParentColumn);
                                fk.Rows[i].Cells[3].Paragraphs[0].Append(item.ReferencedTable);
                                fk.Rows[i].Cells[4].Paragraphs[0].Append(item.ReferencedColumn);

                                i++;
                            }


                            p = document.InsertParagraph("Chaves Estrangeiras").FontSize(15d).SpacingAfter(15d);
                            p.StyleId = "Heading3";


                            // Insert the Table after the Paragraph.
                            p.InsertTableAfterSelf(fk);
                        }
                    }






                    document.Save();

                }
            }
        }

        public static void GenerateProceduresDoc(string path, string outputfile, string headertitle = "", string subtitle = "Procedures e Funções")
        {
            HeaderTitle = headertitle;
            SubTitle = subtitle;
            if (String.IsNullOrWhiteSpace(outputfile))
                OutPutFile = headertitle + "_PROCEDURES";
            else
                OutPutFile = outputfile;


            string procedures = Path.Combine(path, "procedures.xml");
            XmlDocument doc = new XmlDocument();
            XmlNodeList node = doc.SelectNodes("//root/table");

            List<ProceduresModel> procs = new List<ProceduresModel>();
            if (File.Exists(procedures))
            {
                doc.Load(procedures);

                node = doc.SelectNodes("//root/procedure");
                for (int i = 0; i < node.Count; i++)
                {
                    string pname = node[i].SelectSingleNode("@objectname").Value.Trim();
                    string pdesc = node[i].SelectSingleNode("@description").Value.Trim();
                    string ptype = node[i].SelectSingleNode("@type").Value.Trim();

                    ProceduresModel item = new ProceduresModel(pname, pdesc, ptype);
                    var columns = node[i].SelectNodes("item");
                    for (int j = 0; j < columns.Count; j++)
                    {
                        ColumnsModel column = new ColumnsModel();
                        column.Order = columns[j].SelectSingleNode("@id").Value.Trim();
                        column.Name = columns[j].SelectSingleNode("@name").Value.Trim();
                        column.Description = columns[j].SelectSingleNode("@description").Value.Trim();
                        column.Output = columns[j].SelectSingleNode("@output").Value.Trim();
                        column.DataType = columns[j].SelectSingleNode("@datatype").Value.Trim();
                        column.Size = columns[j].SelectSingleNode("@size").Value.Trim();

                        item.Columns.Add(column);
                    }
                    procs.Add(item);
                }
            }
            if (procs.Count > 0)
            {
                /*
                 *  CAPA DO DOCUMENTO
                 */
                string _outfile = Path.Combine(PathOut, OutPutFile);
                if (File.Exists(_outfile))
                    File.Delete(_outfile);

                using (var document = DocX.Create(_outfile))
                {
                    document.AddHeaders();
                    document.AddFooters();
                    document.DifferentFirstPage = true;
                    document.DifferentOddAndEvenPages = true;

                    // Add a simple image from disk.
                    var image = document.AddImage(Path.Combine(ImagePath, ImageHeader));

                    // Set Picture Height and Width.
                    var picture = image.CreatePicture(250, 250);

                    var capa = document.InsertParagraph("").SpacingBefore(200d);
                    capa.Alignment = Alignment.center;
                    capa.AppendPicture(picture);
                    //capa.InsertPicture(picture);
                    //picture.HorizontalAlignment = PictureHorizontalAlignment.CenteredRelativeToPage;
                    //// Set vertical alignement with an offset from top of paragraph.
                    //picture.VerticalOffsetAlignmentFrom = PictureVerticalOffsetAlignmentFrom.Paragraph;
                    //picture.VerticalOffset = 25d;
                    // Insert Picture in paragraph.
                    var t1 = document.InsertParagraph("Mapeamento de Objetos").SpacingAfter(20d).FontSize(14d).Font(new Xceed.Document.NET.Font("Calibri")).Bold();
                    t1.Color(System.Drawing.ColorTranslator.FromHtml("#006790"));
                    t1.Alignment = Alignment.center;

                    t1 = document.InsertParagraph(SubTitle).SpacingAfter(20d).FontSize(13d).Font(new Xceed.Document.NET.Font("Calibri")).Bold();
                    t1.Color(System.Drawing.ColorTranslator.FromHtml("#006790"));
                    t1.Alignment = Alignment.center;

                    var t2 = document.InsertParagraph(HeaderTitle + " em " + DateTime.Now.ToString("dd-MM-yyyy")).SpacingAfter(20d).FontSize(12d).Font(new Xceed.Document.NET.Font("Calibri")).Bold();
                    t2.Color(System.Drawing.ColorTranslator.FromHtml("#006790"));
                    t2.Alignment = Alignment.center;
                    t2.InsertPageBreakAfterSelf();

                    document.AddHeaders();
                    document.AddFooters();
                    document.DifferentFirstPage = true;
                    document.DifferentOddAndEvenPages = true;
                    //t1.InsertPicture(picture).SpacingBefore(20d).InsertPageBreakAfterSelf();

                    //#006790

                    document.InsertParagraph("ÍNDICE").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;
                    var tocSwitches = new Dictionary<TableOfContentsSwitches, string>()
        {
          { TableOfContentsSwitches.O, "1-3"},
          { TableOfContentsSwitches.U, ""},
          { TableOfContentsSwitches.Z, ""},
          { TableOfContentsSwitches.H, ""},
        };
                    document.InsertTableOfContents("Objetos de Dados", tocSwitches);
                    document.InsertParagraph().InsertPageBreakAfterSelf();

                    document.InsertSectionPageBreak();
                    //document.AddHeaders();
                    //document.AddFooters();
                    //document.DifferentFirstPage = true;
                    //document.DifferentOddAndEvenPages = true;
                    //document.Headers.First.InsertParagraph(HeaderTitle + " w ");
                    //document.Headers.Even.InsertParagraph(HeaderTitle);
                    //document.Headers.Odd.InsertParagraph(HeaderTitle);

                    //document.Footers.First.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal);
                    //document.Footers.Even.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal);
                    //document.Footers.Odd.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal);




                    document.InsertSectionPageBreak();
                    //document.DifferentFirstPage = true;

                    document.Sections[1].AddHeaders();
                    document.Sections[1].AddFooters();


                    var headers1 = document.Sections[1].Headers;
                    headers1.First.InsertParagraph(HeaderTitle);
                    headers1.Even.InsertParagraph(HeaderTitle);
                    headers1.Odd.InsertParagraph(HeaderTitle);


                    var footers1 = document.Sections[1].Footers;
                    footers1.First.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.right;

                    footers1.Even.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.right;

                    footers1.Odd.InsertParagraph("Página ").AppendPageNumber(PageNumberFormat.normal).Append(" de ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.right;




                    var h1 = document.InsertParagraph();
                    h1.Append("Procedures e Functions");
                    h1.StyleId = "Heading1";

                    foreach (ProceduresModel tb in procs)
                    {

                        var p = document.InsertParagraph();
                        p.Append(tb.Name);
                        p.StyleId = "Heading2";

                        p = document.InsertParagraph();
                        p.Append(tb.Type).Font(new Xceed.Document.NET.Font("Calibri"))
    .FontSize(12)
    .Color(Color.Blue)
    .Bold()
    .Append(" " + tb.Description).Font(new Xceed.Document.NET.Font("Calibri")).Color(Color.Gray).Italic()
    .SpacingAfter(40);
                        // Insert another Paragraph into this document.

                        var t = document.AddTable(tb.Columns.Count + 1, 6);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.LightGridAccent1;
                        t.AutoFit = AutoFit.ColumnWidth;
                        t.SetWidths(new float[] { 60, 210, 180, 70, 400, 100 }, true);



                        t.Rows[0].Cells[0].Paragraphs[0].Append("#");
                        t.Rows[0].Cells[1].Paragraphs[0].Append("Campo");
                        t.Rows[0].Cells[2].Paragraphs[0].Append("Tipo");
                        t.Rows[0].Cells[3].Paragraphs[0].Append("I/O");
                        t.Rows[0].Cells[4].Paragraphs[0].Append("Descrição");
                        t.Rows[0].Cells[5].Paragraphs[0].Append("Comentário");
                        int i = 1;
                        foreach (var item in tb.Columns)
                        {
                            t.Rows[i].Cells[0].Paragraphs[0].Append(item.Order);
                            t.Rows[i].Cells[1].Paragraphs[0].Append(item.Name);

                            if (!(String.IsNullOrEmpty(item.Size) || string.IsNullOrWhiteSpace(item.Size)))
                            {
                                t.Rows[i].Cells[2].Paragraphs[0].Append(item.DataType + "(" + item.Size + ")");
                            }
                            else
                                t.Rows[i].Cells[2].Paragraphs[0].Append(item.DataType);
                            t.Rows[i].Cells[3].Paragraphs[0].Append(item.Output);
                            t.Rows[i].Cells[4].Paragraphs[0].Append(item.Description);
                            t.Rows[i].Cells[5].Paragraphs[0].Append("");
                            i++;
                        }

                        var st3 = document.InsertParagraph().SpacingAfter(20);
                        st3.Append("Parâmetros");
                        st3.StyleId = "Heading3";
                        st3.InsertTableAfterSelf(t);

                    }

                    document.Save();
                }
            }
        }
    }


    public class TablesModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Comentário descritivo da Tabela
        /// </summary>
        public string Comments { get; set; }

        public List<ColumnsModel> Columns { get; set; }

        public List<IndexModel> Indexes { get; set; }
        public List<ForeignKeyModel> ForeignKeys { get; set; }


        public TablesModel()
        {
            this.Columns = new List<ColumnsModel>();
            this.Indexes = new List<IndexModel>();
            this.ForeignKeys = new List<ForeignKeyModel>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm">Nome</param>
        /// <param name="ds">Descrição</param>
        /// <param name="comments">Comentários</param>
        public TablesModel(string nm, string ds, string comments)
        {
            this.Name = nm;
            this.Description = ds;
            this.Comments = comments;
            this.Columns = new List<ColumnsModel>();
            this.Indexes = new List<IndexModel>();
            this.ForeignKeys = new List<ForeignKeyModel>();
        }
    }

    public class ProceduresModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }

        public List<ColumnsModel> Columns { get; set; }

        public ProceduresModel(string nm, string ds, string type)
        {
            this.Name = nm;
            this.Description = ds;
            this.Type = type;
            this.Columns = new List<ColumnsModel>();

        }
    }
    public class ColumnsModel
    {

        public string Order { get; set; }
        public string Size { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public string Description { get; set; }

        public string Output { get; set; }

        public bool IsNullable { get; set; }
        public bool IsKey { get; set; } = false;
        public bool IsIdentity { get; set; } = false;

        public string Comments { get; set; } = "";
        /// <summary>
        /// Sistema vinculado
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// Formato de Apresentação do Campo
        /// </summary>
        public string Format { get; set; }

    }
    /// <summary>
    /// Tables
    /// </summary>
    public class TableModel
    {
        public string TableName { get; set; }
        public string TableDescription { get; set; }
        public string ColumnOrder { get; set; }
        public string ColumnSize { get; set; }

        public string ColumnName { get; set; }

        public string ColumnDataType { get; set; }

        public string ColumnDescription { get; set; }

        public string IsNullable { get; set; }
        public string ColumnKey { get; set; }

    }

    /// <summary>
    /// Indexes
    /// </summary>
    public class IndexModel
    {
        public int IndexKey { get; set; }

        /// <summary>
        /// Nome do Índice
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nome da Coluna Associada ao Indice
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Tipo de Índice
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Define se é chave primária
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Define se o índice é Unique
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// Define se o índice é uma Unique constraint 
        /// </summary>
        public bool IsUniqueConstraint { get; set; }
    }

    public class ForeignKeyModel
    {
        public string Name { get; set; }

        public string ParentTable { get; set; }

        public string ParentColumn { get; set; }

        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }

    }
}
