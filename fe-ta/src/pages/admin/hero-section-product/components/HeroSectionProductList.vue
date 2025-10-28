<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <template #actions="{ row }">
        <el-button-group class="ml-4">
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button type="primary" :icon="Share" />
          <el-button
            type="primary"
            :icon="Delete"
            @click="handleDelete(row.id)"
          />
        </el-button-group>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, watch } from "vue";
import type {
  VxeGridListeners,
  VxeGridProps,
  VxeGridPropTypes,
} from "vxe-table";
import type {
  HeroSectionProductFilterRequest,
  HeroSectionProductResponse,
} from "@/models/HeroSectionProduct";
import heroSectionProductApi from "@/services/heroSectionProductApi";

import { Delete, Edit, Share } from "@element-plus/icons-vue";

const props = defineProps<{
  heroSectionProduct?: HeroSectionProductResponse[];
  loading?: boolean;
}>();

const emit = defineEmits<{
  (e: "edit-hero-section-product", hero: HeroSectionProductResponse): void;
  (e: "delete-hero-section-product", id: number): void;
}>();

const handleEdit = (hero: HeroSectionProductResponse) => {
  emit("edit-hero-section-product", hero);
};

const handleDelete = (id: number) => {
  emit("delete-hero-section-product", id);
};

const gridRef = ref<any>(null);
defineExpose({ gridRef });

const loading = ref(false);

const handleGetHeroSectionProducts = async () => {
  loading.value = true;
  try {
    return await heroSectionProductApi.getAllHeroSectionProducts();
  } catch (error) {
    console.error("Lỗi khi lấy HeroSectionProducts:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchHeroSectionProducts = async (
  request: HeroSectionProductFilterRequest
) => {
  loading.value = true;
  try {
    return await heroSectionProductApi.filterHeroSectionProducts(request);
  } catch (error) {
    console.error("Lỗi khi filter HeroSectionProducts:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const filterData = reactive<HeroSectionProductFilterRequest>({
  heroSectionTitle: undefined,
  productName: undefined,
  updateTimeFrom: undefined,
  updateTimeTo: undefined,
  note: "",
});

const gridOptions = reactive<
  VxeGridProps<HeroSectionProductResponse> & {
    pagerConfig: VxeGridPropTypes.PagerConfig;
  }
>({
  border: true,
  stripe: true,
  showOverflow: "title",
  height: "100%",
  formConfig: {
    titleWidth: 80,
    titleAlign: "right",
    data: filterData,
    items: [
      {
        field: "heroSectionTitle",
        title: "Hero Section ",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập Hero Section title" },
        },
      },
      {
        field: "productName",
        title: "Product Name",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập Product Name" },
        },
      },
      {
        field: "createdName",
        title: "Người tạo",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên người tạo" },
        },
      },
      {
        field: "updatedName",
        title: "Người chỉnh sửa",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên người chỉnh sửa" },
        },
      },
      {
        field: "note",
        title: "Ghi chú",
        span: 12,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập ghi chú" },
        },
      },
      {
        span: 6,
        itemRender: {
          name: "VxeButtonGroup",
          options: [
            { type: "submit", content: "Tìm kiếm", status: "primary" },
            { type: "reset", content: "Làm mới" },
          ],
        },
      },
    ],
  },
  columns: [
    { type: "seq", width: 60, title: "#" },
    {
      field: "heroSection.title",
      title: "Hero Section",
      minWidth: 150,
      formatter: ({ row }) => row.heroSection?.title || "-",
    },
    {
      field: "product.productName",
      title: "Sản phẩm",
      minWidth: 200,
      formatter: ({ row }) => row.product?.productName || "-",
    },
    { field: "note", title: "Ghi chú", minWidth: 150 },
    { field: "createdName", title: "Người tạo", minWidth: 180 },
    { field: "updatedName", title: "Người chỉnh sửa", minWidth: 180 },
    { field: "updateTime", title: "Thời gian sửa", minWidth: 180 },
    {
      field: "actions",
      title: "Hành động",
      width: 180,
      align: "center",
      fixed: "right",
      slots: { default: "actions" },
    },
  ],
  pagerConfig: { pageSize: 10, pageSizes: [10, 20, 50, 100] },
  proxyConfig: {
    form: true,
    response: { result: "data", total: "total" },
    ajax: {
      async query({ form }) {
        const cleanForm = Object.fromEntries(
          Object.entries(form).filter(
            ([, v]) => v !== null && v !== "" && v !== undefined
          )
        );
        const isSearching = Object.keys(cleanForm).length > 0;
        const data = isSearching
          ? await handleSearchHeroSectionProducts(cleanForm)
          : await handleGetHeroSectionProducts();
        return { data, total: data.length };
      },
    },
  },
});

watch(
  () => props.heroSectionProduct,
  (val) => {
    if (val && Array.isArray(val)) {
      // @ts-ignore
      gridOptions.data = val;
      // @ts-ignore
      gridOptions.proxyConfig = undefined;
    }
  },
  { immediate: true }
);

const gridEvents: VxeGridListeners = {
  proxyQuery() {
    console.log("proxy query chạy");
  },
};
</script>
