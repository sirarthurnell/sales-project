export interface Sale {
    id: string;
    region: string;
    country: string;
    itemType: string;
    salesChannel: string;
    orderPriority: string;
    orderDate: string;
    orderId: number;
    shipDate: string;
    unitsSold: number;
    unitPrice: number;
    unitCost: number;
    totalRevenue: number;
    totalCost: number;
    totalProfit: number;
}
