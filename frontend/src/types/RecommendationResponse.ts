import type { Product } from "./Product";
type RecommendationResponse = {
    weatherSummary: string;
    category: string;
    products: Product[];
};