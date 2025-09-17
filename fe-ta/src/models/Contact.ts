export interface ContactRequest {
  fullname: string;
  email?: string;
  phone?: string;
  address?: string;
  userNote?: string;
  productId?: number;
  status?: string;
  handleByAdminId?: number;
  handleTime?: string; // ISO date string
  adminNote?: string;
  writeIUid?: number;
  updateTime?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
}

export interface ContactResponse {
  id: number;
  fullname: string;
  email?: string;
  phone?: string;
  address?: string;
  userNote?: string;
  productId?: number;
  status?: string;
  createTime: string; // ISO string
  handleByAdminId?: number;
  handleTime?: string;
  adminNote?: string;
  writeIUid?: number;
  updateTime?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
}
