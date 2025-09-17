export interface AnalyticsVisitRequest {
  productId?: number;
  sessionId?: string;
  ipAddress?: string;
  userAgent?: string;
  eventType?: string; 
}

export interface AnalyticsVisitResponse {
  id: number;
  productId?: number;
  sessionId?: string;
  ipAddress?: string;
  userAgent?: string;
  eventType?: string;
  createTime: string; 
}
