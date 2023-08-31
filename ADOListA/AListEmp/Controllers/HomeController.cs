using AListEmp.Models;
using AListEmp.Repositorios.Contrato;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AListEmp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Departamento> _departamentoRepository;
        private readonly IGenericRepository<Empleado> _empleadoRepository;
        public HomeController(ILogger<HomeController> logger, 
            IGenericRepository<Departamento> departamentoRepository,
            IGenericRepository<Empleado> empleadoRepository)
        {
            _logger = logger;
            _departamentoRepository=_departamentoRepository;
            _empleadoRepository= _empleadoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> listaDepartamentos()
        {
            List<Departamento> _lista = await _departamentoRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]
        public async Task<IActionResult> listaEmpleados()
        {
            List<Empleado> _lista = await _empleadoRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarEmpleados([FromBody] Empleado modelo)
        {
          bool _resultado = await _empleadoRepository.Guardar(modelo);

          if(_resultado)
             return StatusCode(StatusCodes.Status200OK, new {valor=_resultado,msg="ok"});
          else
             return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditarEmpleados([FromBody] Empleado modelo)
        {
            bool _resultado = await _empleadoRepository.Editar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarEmpleados(int IdEmpleado)
        {
            bool _resultado = await _empleadoRepository.Eliminar(IdEmpleado);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}