export interface HeroSection {
  ID: number;
  title: string;
  ShortDescription: string;
  HeroMediaUrl: string;
  heroMediaType: "image" | "video";
  isPublished: boolean;
  publishFrom: string; // ISO string
  publishTo?: string; // ISO string optional
}

export interface HeroSectionProduct {
  ID: number;
  HeroSectionId: number;
  productId: number;
  orderIndex: number;
}

export const heroSections: HeroSection[] = [
  {
    ID: 1,
    title: "Summer Collection 2025",
    ShortDescription: "Bộ sưu tập hè nổi bật, thời trang và công nghệ",
    HeroMediaUrl:
      "https://png.pngtree.com/background/20240114/original/pngtree-gadgets-in-a-striking-3d-dim-environment-picture-image_7276667.jpg",
    heroMediaType: "image",
    isPublished: true,
    publishFrom: "2025-09-01T00:00:00",
    publishTo: "2025-12-31T23:59:59",
  },
  {
    ID: 2,
    title: "Smartphone Sale Week",
    ShortDescription: "Giảm giá cực sốc cho các dòng smartphone hot",
    HeroMediaUrl: "/assets/hero2.mp4",
    heroMediaType: "video",
    isPublished: true,
    publishFrom: "2025-09-10T00:00:00",
    publishTo: "2025-09-20T23:59:59",
  },
  {
    ID: 3,
    title: "Gaming Gear Highlights",
    ShortDescription: "Chuột, bàn phím và tai nghe gaming hot nhất",
    HeroMediaUrl: "/assets/hero3.jpg",
    heroMediaType: "image",
    isPublished: true,
    publishFrom: "2025-09-05T00:00:00",
    publishTo: "2025-12-31T23:59:59",
  },
  {
    ID: 4,
    title: "Laptop Deals",
    ShortDescription: "Top laptop giảm giá mạnh trong tháng này",
    HeroMediaUrl: "/assets/hero4.mp4",
    heroMediaType: "video",
    isPublished: true,
    publishFrom: "2025-09-01T00:00:00",
    publishTo: "2025-12-31T23:59:59",
  },
];

export const heroSectionProducts: HeroSectionProduct[] = [
  // HeroSection 1
  { ID: 1, HeroSectionId: 1, productId: 101, orderIndex: 1 },
  { ID: 2, HeroSectionId: 1, productId: 102, orderIndex: 2 },
  { ID: 3, HeroSectionId: 1, productId: 103, orderIndex: 3 },
  { ID: 4, HeroSectionId: 1, productId: 104, orderIndex: 4 },

  // HeroSection 2
  { ID: 5, HeroSectionId: 2, productId: 201, orderIndex: 1 },
  { ID: 6, HeroSectionId: 2, productId: 202, orderIndex: 2 },
  { ID: 7, HeroSectionId: 2, productId: 203, orderIndex: 3 },
  { ID: 8, HeroSectionId: 2, productId: 204, orderIndex: 4 },

  // HeroSection 3
  { ID: 9, HeroSectionId: 3, productId: 301, orderIndex: 1 },
  { ID: 10, HeroSectionId: 3, productId: 302, orderIndex: 2 },
  { ID: 11, HeroSectionId: 3, productId: 303, orderIndex: 3 },
  { ID: 12, HeroSectionId: 3, productId: 304, orderIndex: 4 },

  // HeroSection 4
  { ID: 13, HeroSectionId: 4, productId: 401, orderIndex: 1 },
  { ID: 14, HeroSectionId: 4, productId: 402, orderIndex: 2 },
  { ID: 15, HeroSectionId: 4, productId: 403, orderIndex: 3 },
  { ID: 16, HeroSectionId: 4, productId: 404, orderIndex: 4 },
];
