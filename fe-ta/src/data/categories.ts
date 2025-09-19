export const categories = [
  {
    name: "ĐIỆN THOẠI",
    slug: "dien-thoai",
    brands: [
      "Apple",
      "Samsung",
      "Xiaomi",
      "Oppo",
      "Vivo",
      "Realme",
      "OnePlus",
      "Asus",
      "Google Pixel",
      "Sony",
      "Huawei",
    ],
  },
  {
    name: "LAPTOP",
    slug: "laptop",
    brands: ["Dell", "HP", "Lenovo", "Asus", "MSI"],
  },
  {
    name: "PHỤ KIỆN",
    slug: "phu-kien",
    brands: ["Chuột", "Bàn phím", "Sạc dự phòng", "Ốp lưng"],
  },
  {
    name: "TV",
    slug: "tv",
    brands: ["Samsung", "LG", "Sony"],
  },
  {
    name: "TAI NGHE",
    slug: "tai-nghe",
    brands: ["Sony", "Apple", "JBL", "Anker"],
  },
  {
    name: "MÀN HÌNH",
    slug: "man-hinh",
    brands: ["LG", "Dell", "Samsung", "AOC"],
  },
];

export function slugifyBrand(brand: string) {
  return brand.toLowerCase().replace(/\s+/g, '-');
}

export default { categories, slugifyBrand };
