<template>
  <div class="demo-page-wrapper" style="z-index: 0;">
    <vxe-grid v-bind="gridOptions" v-on="gridEvents">
      <template #actions="{ row }">
        <button
          class="px-3 py-1 bg-black text-white rounded hover:bg-blue-600 mr-2"
          @click="handleEdit(row)"
        >
          Sửa
        </button>
        <button
          class="px-3 py-1 bg-black text-white rounded hover:bg-red-600"
          @click="handleDelete(row.id)"
        >
          Xoá
        </button>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import type { HeroSectionFilterRequest, HeroSectionResponse } from "@/models/HeroSection";
import heroSectionApi from "@/services/heroSectionApi";
import { reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

const props = defineProps<{
  items?: HeroSectionResponse[];
}>();

const emit = defineEmits<{
  (e: "edit-hero-section", hero: HeroSectionResponse): void;
  (e: "delete-hero-section", id: number): void;
}>();

const handleEdit = (hero: HeroSectionResponse) => {
  emit("edit-hero-section", hero);
};

const handleDelete = (id: number) => {
  emit("delete-hero-section", id);
};

const loading = ref(false);

const handleGetHeroSections = async () => {
  loading.value = true;
  try {
    const response = await heroSectionApi.getAllHeroSections();
    return response;
  } catch (error) {
    console.error("Lỗi khi lấy danh sách hero section:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchHeroSections = async (request: HeroSectionFilterRequest) => {
  loading.value = true;
  try {
    const response = await heroSectionApi.filterHeroSections(request);
    return response;
  } catch (error) {
    console.error("Lỗi khi tìm kiếm hero section:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const gridOptions = reactive<
  VxeGridProps<HeroSectionResponse> & {
    pagerConfig: VxeGridPropTypes.PagerConfig;
  }
>({
  border: true,
  stripe: true,
  showOverflow: "title",
  height: "100%",
  formConfig: {
    titleWidth: 100,
    titleAlign: "right",
    data: {
      title: "",
      subtitle: "",
      description: "",
      buttonText: "",
      buttonLink: "",
      isPublished: null,
      publishFrom: null,
      publishTo: null,
    },
    items: [
      {
        field: "title",
        title: "Tiêu đề",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tiêu đề" },
        },
      },
      {
        field: "subtitle",
        title: "Phụ đề",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Nhập phụ đề" } },
      },
      {
        field: "description",
        title: "Mô tả",
        span: 12,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập mô tả", type: "textarea", rows: 2 },
        },
      },
      {
        field: "buttonText",
        title: "Nút bấm",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Nhập text nút" } },
      },
      {
        field: "buttonLink",
        title: "Link nút",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập link nút" },
        },
      },
      {
        field: "isPublished",
        title: "Trạng thái",
        span: 6,
        itemRender: {
          name: "VxeSelect",
          options: [
            { label: "Hoạt động", value: true },
            { label: "Ngưng", value: false },
          ],
        },
      },
      {
        field: "publishFrom",
        title: "Ngày bắt đầu",
        span: 6,
        itemRender: { name: "VxeInput", props: { type: "date" } },
      },
      {
        field: "publishTo",
        title: "Ngày kết thúc",
        span: 6,
        itemRender: { name: "VxeInput", props: { type: "date" } },
      },
      {
        span: 6,
        collapseNode: true,
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
    { field: "title", title: "Tiêu đề", minWidth: 150 },
    { field: "subtitle", title: "Phụ đề", minWidth: 150 },
    { field: "description", title: "Mô tả", minWidth: 200 },
    { field: "buttonText", title: "Nút bấm", minWidth: 120 },
    { field: "buttonLink", title: "Link nút", minWidth: 150 },
    {
      field: "isPublished",
      title: "Trạng thái",
      minWidth: 100,
      formatter: ({ cellValue }) => (cellValue ? "Hoạt động" : "Ngưng"),
    },
    { field: "publishFrom", title: "Bắt đầu", minWidth: 130 },
    { field: "publishTo", title: "Kết thúc", minWidth: 130 },
    {
      field: "heroMediaUrl",
      title: "Media",
      minWidth: 150,
      formatter: ({ cellValue }) =>
        cellValue ? `<img src="${cellValue}" width="80"/>` : "Không có",
    },
    {
      field: "actions",
      title: "Hành động",
      width: 180,
      align: "center",
      fixed: "right",
      slots: { default: "actions" },
    },
  ],
  pagerConfig: {
    pageSize: 10,
    pageSizes: [10, 20, 50, 100],
  },
  proxyConfig: {
    form: true,
    response: {
      result: "data",
      total: "total",
    },
    ajax: {
      async query({ form }) {
        const cleanForm = Object.fromEntries(
          Object.entries(form).filter(
            ([_, v]) => v !== null && v !== "" && v !== undefined
          )
        );

        const isSearching = Object.keys(cleanForm).length > 0;

        const data = isSearching
          ? await handleSearchHeroSections(cleanForm)
          : await handleGetHeroSections();

        return {
          data,
          total: data.length,
        };
      },
    },
  },
});

watch(
  () => props.items,
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
