<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <!-- Cột hành động -->
      <template #actions="{ row }">
        <el-button-group class="ml-4">
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button
            type="primary"
            :icon="Delete"
            @click="handleDelete(row.id)"
          />
        </el-button-group>
      </template>

      <!-- Cột logo -->
      <template #logo="{ row }">
        <img
          v-if="row.logoUrl"
          :src="row.logoUrl"
          alt="Logo"
          style="max-width: 80px; max-height: 80px; object-fit: contain"
        />
        <span v-else>Chưa có logo</span>
      </template>
      <template #imgDefault="{ row }">
        <img
          v-if="row.imgDefaultUrl"
          :src="row.imgDefaultUrl"
          alt="Logo"
          style="max-width: 80px; max-height: 80px; object-fit: contain"
        />
        <span v-else>Chưa có logo</span>
      </template>
      <template #imgHover="{ row }">
        <img
          v-if="row.imgHoverUrl"
          :src="row.imgHoverUrl"
          alt="Logo"
          style="max-width: 80px; max-height: 80px; object-fit: contain"
        />
        <span v-else>Chưa có logo</span>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import type {
  PartnerFilterRequest,
  PartnerResponse,
} from "@/models/Partner";
import { onMounted, reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";
import { Delete, Edit } from "@element-plus/icons-vue";
import { usePartnerStore } from "@/store/partnerStore";

// === Props + Emits ===
const props = defineProps<{
  partners?: PartnerResponse[];
  loading?: boolean;
}>();

const emit = defineEmits<{
  (e: "edit-partner", partner: PartnerResponse): void;
  (e: "delete-partner", id: number): void;
}>();

const handleEdit = (partner: PartnerResponse) => {
  emit("edit-partner", partner);
};

const handleDelete = (id: number) => {
  emit("delete-partner", id);
};

// === Store ===
const partnerStore = usePartnerStore();
const gridRef = ref<any>(null);
defineExpose({ gridRef });

// Load dữ liệu khi mount
onMounted(() => {
  if (partnerStore.partners.length === 0) {
    partnerStore.fetchPartners();
  }
});

// Form filter
const filterData = reactive<PartnerFilterRequest>({
  name: "",
  isActive: undefined,
  createdName: "",
  updatedName: "",
  updateTimeFrom: "",
  updateTimeTo: "",
  note: "",
});

// Cấu hình lưới
const gridOptions = reactive<
  VxeGridProps<PartnerResponse> & {
    pagerConfig: VxeGridPropTypes.PagerConfig;
  }
>({
  border: true,
  stripe: true,
  height: "100%",
  showOverflow: "title",
  formConfig: {
    titleWidth: 80,
    titleAlign: "center",
    data: filterData,
    items: [
      {
        field: "name",
        title: "Tên đối tác",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Nhập tên đối tác" } },
      },
      {
        field: "isActive",
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
        field: "createdName",
        title: "Người tạo",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Người tạo" } },
      },
      {
        field: "updatedName",
        title: "Người cập nhật",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Người cập nhật" } },
      },
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
    {
      field: "logoUrl",
      title: "Logo",
      width: 120,
      align: "center",
      slots: { default: "logo" },
    },
    {
      field: "imgDefaultUrl",
      title: "Ảnh mặc định",
      width: 160,
      align: "center",
      slots: { default: "imgDefault" },
    },
    {
      field: "imgHoverUrl",
      title: "Ảnh Hover",
      width: 160,
      align: "center",
      slots: { default: "imgHover" },
    },
    { field: "slug", title: "Slug", minWidth: 100, sortable: true },
    { field: "name", title: "Tên đối tác", minWidth: 200, sortable: true },
    { field: "link", title: "Liên kết", minWidth: 200, sortable: true },
    {
      field: "isActive",
      title: "Trạng thái",
      width: 130,
      formatter: ({ cellValue }) => (cellValue ? "Hoạt động" : "Ngưng"),
      sortable: true,
    },
    { field: "orderIndex", title: "Thứ tự", width: 100, align: "center" },
    { field: "createdName", title: "Người tạo", minWidth: 160 },
    { field: "updatedName", title: "Người cập nhật", minWidth: 160 },
    {
      field: "actions",
      title: "Hành động",
      width: 140,
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
          Object.entries(form).filter(([_, v]) => v !== null && v !== "" && v !== undefined)
        );
        const isSearching = Object.keys(cleanForm).length > 0;

        const data = isSearching
          ? (await partnerStore.filterPartners(cleanForm), partnerStore.partners)
          : (await partnerStore.fetchPartners(), partnerStore.partners);

        return {
          data,
          total: data.length,
        };
      },
    },
  },
});

// Nếu có prop partners thì ưu tiên hiển thị
watch(
  () => props.partners,
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
    console.log("Đang query dữ liệu Partner qua store...");
  },
};
</script>
