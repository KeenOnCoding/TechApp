import { Component, OnInit } from '@angular/core';
import { CartService } from '../../shared/services/cart.service';
import { Observable, of } from 'rxjs';
import { CartItem } from 'src/app/modals/cart-item';
import { ProductService } from '../../shared/services/product.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IOrder } from '../../../modals/order.model';
@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.sass']
})
export class CheckoutComponent implements OnInit {

  public cartItems: Observable<CartItem[]> = of([]);
  public buyProducts: CartItem[] = [];
  newOrderForm: FormGroup;  // new order form
  amount: number;
  payments: string[] = ['Create an Account?', 'Flat Rate'];
  paymantWay: string[] = ['Direct Bank Transfer', 'PayPal'];
  order: IOrder = {} as IOrder;
  constructor(private cartService: CartService, public productService: ProductService, fb: FormBuilder) {
    //this.newOrderForm = fb.group({
    //  'firstName': [this.order.firstName, Validators.required],
    //  'lasttName': [this.order.lasttName, Validators.required],
    //  'email': [this.order.email, Validators.required],
    //  'subject': [this.order.subject, Validators.required],
    //  'town': [this.order.town, Validators.required],
    //  'state': [this.order.state, Validators.required],
    //  'postcode': [this.order.postcode, Validators.required],
    //  'phone': [this.order.phone, Validators.required],
    //  'content': [this.order.content, Validators.required],
    //});
  }

  ngOnInit() {
    this.cartItems = this.cartService.getItems();
    this.cartItems.subscribe(products => this.buyProducts = products);
    this.getTotal().subscribe(amount => this.amount = amount);
  }
  submitForm(value: any) { console.log(this.order.firstName) }
  public getTotal(): Observable<number> {
    return this.cartService.getTotalAmount();
    }

}