import { Component, OnInit, ViewChild } from '@angular/core';
import { Product } from 'src/app/modals/product.model';
import { CartItem } from 'src/app/modals/cart-item';
import { CartService } from '../services/cart.service';
import { SidebarMenuService } from '../sidebar/sidebar-menu.service';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent implements OnInit {
  public sidenavMenuItems:Array<any>;

  public currencies = ['USD', 'EUR'];
  public currency:any;
  public flags = [
    { name:'English', image: 'assets/images/flags/gb.svg' },
    { name:'German', image: 'assets/images/flags/de.svg' },
    { name:'French', image: 'assets/images/flags/fr.svg' },
    { name:'Russian', image: 'assets/images/flags/ru.svg' },
    { name:'Turkish', image: 'assets/images/flags/tr.svg' }
  ]
  public flag:any;

  products: Product[];

  indexProduct: number;
  shoppingCartItems: CartItem[] = [];

  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Sevet', 'Eight', 'Nine', 'Ten', 'Eleven', 'Twelve'];
  filteredOptions: Observable<string[]>;

  constructor(private cartService: CartService) {
    this.cartService.getItems().subscribe(shoppingCartItems => this.shoppingCartItems = shoppingCartItems);
  }

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value)),
    );
    this.currency = this.currencies[0];
    this.flag = this.flags[0];
  }
  public someFunc(event: any) {

    console.log(event)
  }
  private _filter(value: string): string[] {

    if (!value) {
     return [];
    }
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
  public changeCurrency(currency){
    this.currency = currency;
  }
  public changeLang(flag){
    this.flag = flag;
  }

}
