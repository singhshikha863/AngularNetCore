using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardWebApi.Data;
using CardWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardDbContext _cardDbContext;
        public CardController(CardDbContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
        }

        //Get All cards
        [HttpGet]
        public async  Task<IActionResult> GetAllCards()
        {
            var card = await _cardDbContext.Cards.ToListAsync();
            if (card != null)
            {
                return Ok(card);
            }

            return NotFound("Not found");
        }

        //Get one Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await _cardDbContext.Cards.FirstOrDefaultAsync(x=>x.Id== id);
            if (card!=null)
            {
                return Ok(card);
            }

            return NotFound("Card not found");
        }

        //Get one Card
        [HttpPost]
       // [Route("{id:guid}")]
        //[ActionName("GetCard")]
        public async Task<IActionResult> AddCard(Cards card)
        {
            card.Id = Guid.NewGuid();
            await _cardDbContext.Cards.AddAsync(card);
            await _cardDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new{id= card.Id}, card);
        }

        //Update one Card
        [HttpPut]
        [Route("{id:guid}")]
        //[ActionName("GetCard")]
        public async Task<IActionResult> UpdateCard([FromBody] Cards card, Guid id)
        {
            var existingCard = await _cardDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardHolderName = card.CardHolderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.CVC = card.CVC;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                await _cardDbContext.SaveChangesAsync();
                return Ok(existingCard);

            }

            return NotFound("Card not found");
        }

        //Delete one Card
        [HttpDelete]
        [Route("{id:guid}")]
        //[ActionName("GetCard")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var existingCard = await _cardDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                _cardDbContext.Remove(existingCard);
                await _cardDbContext.SaveChangesAsync();
                return Ok(existingCard);

            }

            return NotFound("Card not found");
        }
    }
}