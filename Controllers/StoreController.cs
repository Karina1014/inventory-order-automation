using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;
using InventoryFlow.Repository;

namespace InventoryFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public StoreController(IStoreRepository storeRepository, IMapper mapper)
    {
        _storeRepository = storeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StoreDto>))]
    public async Task<IActionResult> GetStores()
    {
        var stores = await _storeRepository.GetStoresAsync();
        var storesDto = _mapper.Map<IEnumerable<StoreDto>>(stores);
        return Ok(storesDto);
    }

    [HttpGet("{idStore:int}", Name = "GetStore")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStore(int idStore)
    {
        if (idStore <= 0)
        {
            return BadRequest("El identificador proporcionado no es válido.");
        }

        var store = await _storeRepository.GetStoreAsync(idStore);
        if (store == null)
        {
            return NotFound($"No se encontró ninguna sucursal con el identificador {idStore}.");
        }

        var storeDto = _mapper.Map<StoreDto>(store);
        return Ok(storeDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StoreDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto createStoreDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _storeRepository.StoreExistsAsync(createStoreDto.Name))
        {
            ModelState.AddModelError("Name", "Ya existe una sucursal registrada con ese nombre.");
            return BadRequest(ModelState);
        }

        var store = _mapper.Map<Store>(createStoreDto);

        if (!await _storeRepository.CreateStoreAsync(store))
        {
            ModelState.AddModelError("Error", $"Ocurrió un error al guardar el registro {store.Name}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        var storeDto = _mapper.Map<StoreDto>(store);
        return CreatedAtRoute("GetStore", new { idStore = store.IdStore }, storeDto);
    }

    [HttpPut("{idStore:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStore(int idStore, [FromBody] UpdateStoreDto updateStoreDto)
    {
        if (!ModelState.IsValid || idStore <= 0)
        {
            return BadRequest(ModelState);
        }

        if (!await _storeRepository.StoreExistsAsync(idStore))
        {
            return NotFound($"No se encontró la sucursal con ID {idStore} para actualizar.");
        }

        var store = _mapper.Map<Store>(updateStoreDto);
        store.IdStore = idStore;

        if (!await _storeRepository.UpdateStoreAsync(store))
        {
            ModelState.AddModelError("Error", $"Ocurrió un error al actualizar el registro {store.Name}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{idStore:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteStore(int idStore)
    {
        if (idStore <= 0)
        {
            return BadRequest("Identificador de sucursal no válido.");
        }

        var store = await _storeRepository.GetStoreAsync(idStore);
        if (store == null)
        {
            return NotFound($"No se encontró la sucursal con ID {idStore} para eliminar.");
        }

        if (!await _storeRepository.DeleteStoreAsync(store))
        {
            ModelState.AddModelError("Error", $"Ocurrió un error al eliminar la sucursal con ID {idStore}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
        }

        return NoContent();
    }
}
