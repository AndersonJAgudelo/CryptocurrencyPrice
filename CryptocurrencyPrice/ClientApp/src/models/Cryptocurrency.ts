import { Quote } from "./Quote";

export interface Cryptocurrency {
  id: number;
  name: string;
  symbol: string;
  quote: Quote;
}
