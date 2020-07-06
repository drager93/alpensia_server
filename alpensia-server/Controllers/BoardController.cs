using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alpensia_server.Databases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace alpensia_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : Controller
    {
        protected DBContext Context { get; }

        public BoardController(DBContext context)
        {
            Context = context;
        }

        [HttpGet]
        // GET: /<controller>/
        public IEnumerable<Board> Index()
        {
            return Context.Board.Where(b => b.BrdEndDate >= DateTime.Now).OrderByDescending(o => o.BrdIndex);
        }

        [HttpGet("{index}")]
        public IActionResult GetSingle(int index)
        {
            var prev = new Board();
            var next = new Board();

            var boardList = Index().ToList();
            var findBoard = Context.Board.SingleOrDefault(b => b.BrdIndex == index);

            var now = boardList.Find(f => f.BrdIndex == findBoard.BrdIndex);
            var indexOf = boardList.IndexOf(now);
            prev = boardList.ElementAt(indexOf > 0 ? indexOf - 1 : indexOf);
            next = boardList.ElementAt(indexOf < boardList.Count() - 1 ? indexOf + 1 : indexOf);

            //조회수
            //if (findBoard != null)
            //{
            //    findBoard.BrdViewCount += 1;
            //    Context.SaveChanges();
            //}
            return Ok(new { board = findBoard, next = next, prev = prev });
        }

        [HttpGet("getmain")]
        public IActionResult GetMain()
        {
            return Ok(Index().Where(a => a.BrdIsMain));
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            Context.Board.Add(new Board
            {
                BrdTitle = "test",
                BrdStartDate = DateTime.Now,
                BrdEndDate = DateTime.Now.AddDays(7),
                BrdMainImg = "section2-2.png",
                BrdDetailImg = "section2-2.png"
            });
            Context.SaveChanges();

            return Ok();
        }

        [HttpGet("delete/{index}")]
        public IActionResult Delete(int index)
        {
            var board = Context.Board.SingleOrDefault(a => a.BrdIndex == index);

            if (board != null)
            {
                Context.Board.Remove(board);
                Context.SaveChanges();
            }

            return Ok();
        }

        [HttpGet("update")]
        public IActionResult Update(int index, string title)
        {
            var board = Context.Board.SingleOrDefault(a => a.BrdIndex == index);

            if (board != null)
            {
                board.BrdTitle = title;
                Context.SaveChanges();
            }

            return Ok();
        }
    }
}