using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {

        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(IFormFile image)
        {
            try
            {
                //validação simples para caso não tenha enviado nenhuma imagem para 
                //upload, é retornardo null

                if (image == null) return null;

                //Salvando a imagem no formato enviado pelo usuário
                using (var stream = new FileStream(Path.Combine("Imagens", image.FileName), FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return Ok(new
                {
                    mensagem = "Imagem salva com sucesso",
                    urlImagem = $"http://localhost:5005/img/{image.FileName}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro no upload: " + ex.Message);
            }            
        }
    }
}
