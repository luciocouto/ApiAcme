using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using ApiAcme.Enumerador;
using ApiAcme.Flexcel;
using ApiAcme.Models;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAcme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AcmeContext _context;

        public AuthorsController(AcmeContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        /// <summary>
        ///   /// Get all AuthorItem.
        /// </summary>
        /// <param name="sortOrder">ordenação por firstname, birthdate, asc e desc</param>
        /// <param name="searchString">filtro firstname</param>
        /// <param name="pageNumber">número da pagina</param>
        /// <param name="pageSize">Qtde itens por paginação</param>
        /// <returns></returns>
        [Route("~/api/authors")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authors>>> GetAuthors(SortOrderAuthorEnum sortOrder, string searchString, int? pageNumber, int? pageSize)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }


            if (!pageSize.HasValue)
            {
                pageSize = 5;
            }

            var authors = from s in _context.Authors
                              //.Include(a => a.Posts)
                              //.Include(e => e.Rates)
                              //.AsNoTracking()
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case SortOrderAuthorEnum.FirstName_Desc:
                    authors = authors.OrderByDescending(s => s.FirstName);
                    break;
                case SortOrderAuthorEnum.FirstName_Asc:
                    authors = authors.OrderBy(s => s.FirstName);
                    break;
                case SortOrderAuthorEnum.Birthdate_Desc:
                    authors = authors.OrderByDescending(s => s.Birthdate);
                    break;
                case SortOrderAuthorEnum.Birthdate_Asc:
                    authors = authors.OrderBy(s => s.Birthdate);
                    break;
                default:
                    authors = authors.OrderBy(s => s.FirstName);
                    break;
            }

            
            return await PaginatedList<Authors>.CreateAsync(authors.AsNoTracking(), pageNumber ?? 1, pageSize.Value);
        }

        // GET: api/Authors/5
        [HttpGet("~/api/authors/{id}")]
        public async Task<ActionResult<Authors>> GetAuthors(int id)
        {

            var authors = await _context.Authors
             //.Include(a => a.Posts)
             //.Include(e => e.Rates)
             //.AsNoTracking()
             .FirstOrDefaultAsync(m => m.Id == id);

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("~/api/authors/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutAuthors(int id, Authors authors)
        {
            if (id != authors.Id)
            {
                return BadRequest();
            }

            _context.Entry(authors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("~/api/authors")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Authors>> PostAuthors(Authors authors)
        {
            _context.Authors.Add(authors);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthorsExists(authors.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAuthors", new { id = authors.Id }, authors);
        }

        // DELETE: api/Authors/5
        /// <summary>
        /// Deletes a specific AuthorItem.
        /// </summary>
        /// <param name="id"></param>    
      //  [HttpDelete("{id}")]
        [HttpDelete("~/api/authors/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Authors>> DeleteAuthors(int id)
        {
            var authors = await _context.Authors.FindAsync(id);
            if (authors == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(authors);
            await _context.SaveChangesAsync();

            return authors;
        }

        private bool AuthorsExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        // GET: api/ReportAuthors
        /// <summary>
        ///   /// Get Report with all Authors.
        /// </summary>
        /// <param name="typeReport">geração report em xlsx, pdf, html </param>
        /// <param name="sortOrder">ordenação por firstname, birthdate, asc e desc</param>
        /// <returns></returns>
        [Route("~/api/flexcel_authors")]
        [HttpGet]
        public HttpResponseMessage GetReportAuthors(TypeReportEnum typeReport, SortOrderAuthorEnum sortOrder)
        {
            string nomeArquivo = "AuthorsTeste1.xlsx";
            string path = @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\";
            string pathArquivo = path + nomeArquivo;
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var authors = from s in _context.Authors
                             .Include(a => a.Posts)
                          select s;

            authors = ChangeSortOrder(sortOrder, authors);

            authors = authors.Where(a => a.Posts.Count > 0).Take(10);

            RelatoriosFlexcelUtilitario relatorios = new RelatoriosFlexcelUtilitario();

            relatorios.GerarExcel<Authors>(authors.ToList(), "Authors", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\AuthorsTemplate.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste1.xlsx");
            relatorios.GerarExcel<Authors>(authors.ToList(), "Authors", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\AuthorsTemplate2.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste2.xlsx");

            switch (typeReport)
            {
                case TypeReportEnum.Xlsx:
                    nomeArquivo = "AuthorsTeste1.xlsx";
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case TypeReportEnum.Pdf:
                    nomeArquivo = "AuthorsTeste1.pdf";
                    contentType = "application/pdf";
                    relatorios.ExportToPdf(@"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste1.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste1.pdf");
                    relatorios.ExportToPdf(@"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste2.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste2.pdf");
                    break;
                case TypeReportEnum.Html:
                    nomeArquivo = "AuthorsTeste1.html";
                    contentType = "text/html";
                    relatorios.ExportToHtml(@"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste1.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste1.html");
                    relatorios.ExportToHtml(@"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste2.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\AuthorsTeste2.html");
                    break;
                default:
                    break;
            }

            TesteFlexcelSimples(relatorios);

            if (System.IO.File.Exists(pathArquivo))
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                //TMS
                //Response.Clear();
                //Response.AddHeader("Content-Disposition", "attachment;"
                //+ "filename=Test.xls");
                //Response.AddHeader("Content-Length",
                //ms.Length.ToString());
                //Response.ContentType = "application/excel";
                //Response.BinaryWrite(ms.ToArray());
                //Response.End();

                //var stream = new FileStream(pathArquivo, FileMode.Open);
                //stream.Position = 0;
                //result.Content = new StreamContent(stream);
                //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = pathArquivo };
                //result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contenType);
                //result.Content.Headers.ContentDisposition.FileName = pathArquivo;
                return result;
            }
            else
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.Gone);
                return result;
            }
        }

        private static void TesteFlexcelSimples(RelatoriosFlexcelUtilitario relatorios)
        {
            List<TesteFlexcellProjecao> lista = new List<TesteFlexcellProjecao>();

            lista.Add(new TesteFlexcellProjecao() { ValorString_1 = "Teste1", ValorData_2 = new DateTime(2020, 10, 8), ValorNumero_3 = 12345, ValorDecimal_4 = 10.25M, ValorBool_5 = true });
            lista.Add(new TesteFlexcellProjecao() { ValorString_1 = "Teste2", ValorData_2 = new DateTime(2019, 5, 28), ValorNumero_3 = 1000234, ValorDecimal_4 = -10.25M, ValorBool_5 = false });
            lista.Add(new TesteFlexcellProjecao() { ValorString_1 = "Teste3", ValorData_2 = new DateTime(2017, 12, 12), ValorNumero_3 = 1897653, ValorDecimal_4 = 198653.99999M, ValorBool_5 = true });
            lista.Add(new TesteFlexcellProjecao() { ValorString_1 = "Teste4", ValorData_2 = new DateTime(1989, 4, 1), ValorNumero_3 = 0, ValorDecimal_4 = 999999.999999M, ValorBool_5 = false });
            lista.Add(new TesteFlexcellProjecao() { ValorString_1 = "Teste5", ValorData_2 = new DateTime(1993, 9, 24), ValorNumero_3 = -1256, ValorDecimal_4 = -999999.999999M, ValorBool_5 = false });

            relatorios.GerarExcel<TesteFlexcellProjecao>(lista.ToList(), "Dados", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\TemplatePadrao.xlsx", @"D:\Arquitetura\Poc Microservicos\ApiAcme\RelatoriosTemplates\RelatoriosGerados\FlexcelPadrao.xlsx");
        }

        private static IQueryable<Authors> ChangeSortOrder(SortOrderAuthorEnum sortOrder, IQueryable<Authors> authors)
        {
            switch (sortOrder)
            {
                case SortOrderAuthorEnum.FirstName_Desc:
                    authors = authors.OrderByDescending(s => s.FirstName);
                    break;
                case SortOrderAuthorEnum.FirstName_Asc:
                    authors = authors.OrderBy(s => s.FirstName);
                    break;
                case SortOrderAuthorEnum.Birthdate_Desc:
                    authors = authors.OrderByDescending(s => s.Birthdate);
                    break;
                case SortOrderAuthorEnum.Birthdate_Asc:
                    authors = authors.OrderBy(s => s.Birthdate);
                    break;
                default:
                    authors = authors.OrderBy(s => s.FirstName);
                    break;
            }

            return authors;
        }
    }

    public class TesteFlexcellProjecao
    {
        public string ValorString_1 { get; set;}
        public DateTime ValorData_2 { get; set; }
        public Int32 ValorNumero_3 { get; set; }
        public Decimal ValorDecimal_4 { get; set; }
        public Boolean ValorBool_5 { get; set; }
        public string Cabecalho { get; set; }
    }
}
