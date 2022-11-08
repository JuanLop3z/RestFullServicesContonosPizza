using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {

    }


    //GET all action
    [HttpGet]
    //Receive a List that executes the "GetAll()" method to obtain data from Pizzas
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();


    //GET by Id action
    /*Get the id as a parameter in the request [Http Get"{id}"], two routes differents*/
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {   //Create a variable that receives the id
        var pizza = PizzaService.Get(id);
        /*If the id arrives null then it executes "NotFound()" 
        otherwise it returns the value of pizza found*/
        if (pizza == null)
        {
            //error "NotFound()" return value 404 
            return NotFound();
        }
        return pizza;
    }


    //POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        
        return CreatedAtAction(nameof(Create), new {id = pizza.Id}, pizza);
    }


    //PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if(id != pizza.Id)
            return BadRequest();


        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }


    //DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if(pizza is null)
            return NotFound();

        PizzaService.Delete(id);
        
        return NoContent();
    }

}