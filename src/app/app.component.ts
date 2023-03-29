import { Component, OnInit } from '@angular/core';
import { Card } from './Model/card.model';
import { CardsService } from './service/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'UICard';
  cardDetails: Card[] = [];
  card : Card ={
   id: '',
   cardHolderName:'',
   cardNumber:'',
   expiryMonth:'',
   expiryYear:'',
   cvc:''

  }
  constructor(private cardService : CardsService){

  }
  ngOnInit(): void {
    this.getAllCard();
  }
  getAllCard(){
    this.cardService.getAllCards()
    .subscribe(
      response=>{
        this.cardDetails = response;
      }
    )
  }

  OnSubmit(){
    if (this.card.id == ''){
      this.cardService.addCard(this.card)
      .subscribe( response=>{
      this.getAllCard();
      this.card ={
        id: '',
        cardHolderName:'',
        cardNumber:'',
        expiryMonth:'',
        expiryYear:'',
        cvc:''
      }
      }
        )
    } 
    else{
      this.updateData(this.card);
    }
   
  }

  deleteCard(id: string){
    this.cardService.deleteCard(id)
    .subscribe(Response=>{
      this.getAllCard();
    })    
  }

  updateData(card: Card){
   this.cardService.updateCard(card)
   .subscribe(response=>
    {
      this.getAllCard();
    })
  }
  populateData(card: Card){
   this.card = card;
  }
}
