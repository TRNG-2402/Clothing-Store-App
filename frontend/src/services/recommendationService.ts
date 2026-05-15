import { api } from './api'
import type { Product } from '../types/Product'

export type RecommendationResponse = {
    weatherSummary: string
    category: string
    products: Product[]
}

export async function getRecommendations(
    lat: number,
    lon: number
): Promise<RecommendationResponse>
{
    const response = await api.get(
        `/recommendations?lat=${lat}&lon=${lon}`
    )

    return response.data
}