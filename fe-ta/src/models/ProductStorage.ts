export interface ProductStorageRequest {
  productId: number;            
  storageName: string;          
  additionalPrice?: number;     
  isAvailable?: boolean;        
  note?: string;
}

export interface ProductStorageFilterRequest {
  productId: number;
  productName?: string;
  storageName?: string;
  additionalPrice?: number;
  createdName?: string;
  updatedName?: string;
  fromUpdateTime?: string;
  toUpdateTime?: string;
  note?: string;
}

export interface ProductStorageResponse {
  id: number;
  productId: number;
  productName?: string;
  storageName: string;
  additionalPrice?: number;
  isAvailable: boolean;
  note?: string;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string;
  createdName?: string;
  updatedName?: string;
}
