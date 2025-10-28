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
      <template #media="{ row }">
        <img
          v-if="row.heroMediaUrl"
          :src="row.heroMediaUrl"
          alt="Hero Media"
          style="max-width: 100px; max-height: 100px"
        />
        <span v-else>Chưa có ảnh</span>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import type {
  HeroSectionFilterRequest,
  HeroSectionResponse,
} from "@/models/HeroSection";
import { onMounted, reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

import { Delete, Edit, Share } from "@element-plus/icons-vue";
import { useHeroSectionStore } from "@/store/heroSectionStore";

// === Props + Emits ===
const props = defineProps<{
  heroSections?: HeroSectionResponse[];
  loading?: boolean;
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

// === Store ===
const heroSectionStore = useHeroSectionStore();
const gridRef = ref<any>(null);
defineExpose({ gridRef });


// Load dữ liệu khi mount
onMounted(() => {
  if (heroSectionStore.sections.length === 0) {
    heroSectionStore.loadAll();
  }
});

// Tạo reactive object từ interface
const filterData = reactive<HeroSectionFilterRequest>({
  title: "",
  description: "",
  pageHero: "",
  isPublished: undefined,
  publishFrom: "",
  publishTo: "",
  createdName: "",
  updatedName: "",
});

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
    titleWidth: 80,
    titleAlign: "center",
    data: filterData,
    items: [
      { field: "title", title: "Tiêu đề", span: 6, itemRender: { name: "VxeInput", props: { placeholder: "Nhập tiêu đề" } } },
      { field: "description", title: "Mô tả", span: 6, itemRender: { name: "VxeInput", props: { placeholder: "Nhập mô tả", type: "textarea", rows: 2 } } },
      { field: "pageHero", title: "Page Hero", span: 6, itemRender: { name: "VxeInput", props: { placeholder: "Nhập Page Hero" } } },
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
      { field: "publishFrom", title: "Ngày bắt đầu", span: 6, itemRender: { name: "VxeInput", props: { type: "date" } } },
      { field: "publishTo", title: "Ngày kết thúc", span: 6, itemRender: { name: "VxeInput", props: { type: "date" } } },
      { field: "createdName", title: "Người tạo", span: 6, itemRender: { name: "VxeInput", props: { placeholder: "Người tạo" } } },
      { field: "updatedName", title: "Người cập nhật", span: 6, itemRender: { name: "VxeInput", props: { placeholder: "Người cập nhật" } } },
      {
        span: 6,
        align: "right",
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
    { type: "seq", width: 60, title: "#", align: "center" },
    { field: "title", title: "Tiêu đề", minWidth: 200, sortable: true },
    { field: "description", title: "Mô tả", minWidth: 200, sortable: true },
    { field: "pageHero", title: "Trang hiển thị", minWidth: 140, sortable: true },
    {
      field: "isPublished",
      title: "Trạng thái",
      minWidth: 150,
      formatter: ({ cellValue }) => (cellValue ? "Hoạt động" : "Ngưng"),
      sortable: true,
    },
    { field: "publishFrom", title: "Bắt đầu", minWidth: 180, sortable: true },
    { field: "publishTo", title: "Kết thúc", minWidth: 180, sortable: true },
    { field: "createdName", title: "Người tạo", minWidth: 180, sortable: true },
    { field: "updatedName", title: "Người cập nhật", minWidth: 180, sortable: true },
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
        // Lọc bỏ giá trị rỗng
        const cleanForm = Object.fromEntries(
          Object.entries(form).filter(([_, v]) => v !== null && v !== "" && v !== undefined)
        );

        const isSearching = Object.keys(cleanForm).length > 0;
        const data = isSearching
          ? await heroSectionStore.filter(cleanForm)
          : (await heroSectionStore.loadAll(), heroSectionStore.sections);

        return {
          data,
          total: data.length,
        };
      },
    },
  },
});

// Nếu có prop heroSections thì ưu tiên hiển thị
watch(
  () => props.heroSections,
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
    console.log("proxy query chạy qua store");
  },
};
</script>
