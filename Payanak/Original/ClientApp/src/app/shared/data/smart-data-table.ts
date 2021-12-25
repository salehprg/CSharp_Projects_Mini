// Smart DataTable
export const settings = {
  columns: {
    id: {
      title: 'ID',
      filter: false,
    },
    name: {
      title: 'Full Name',
      filter: false,
    },
    username: {
      title: 'User Name',
      filter: false,
    },
    email: {
      title: 'Email',
      filter: false,
    }
  },
  attr: {
    class: 'table table-responsive'
  },
  edit: {
    editButtonContent: '<i class="ft-edit-2 info font-medium-1 mr-2"></i>'
  },
  delete: {
    deleteButtonContent: '<i class="ft-x danger font-medium-1 mr-2"></i>'
  }
};

export const filtersettings = {
  columns: {
    id: {
      title: 'شناسه',
    },
    title: {
      title: 'نام',
    },
    parent: {
      title: 'سرشاخه',
    },
  },
  attr: {
    class: 'table table-responsive'
  },
};

export const alertsettings = {
  delete: {
    confirmDelete: true,
    deleteButtonContent: '<i class="ft-x danger font-medium-1 mr-2"></i>'
  },
  add: {
    confirmCreate: true,
  },
  edit: {
    confirmSave: true,
    editButtonContent: '<i class="ft-edit-2 info font-medium-1 mr-2"></i>'
  },
  columns: {
    id: {
      title: 'ID',
    },
    name: {
      title: 'Full Name',
    },
    username: {
      title: 'User Name',
    },
    email: {
      title: 'Email',
    },
  },
  attr: {
    class: 'table table-responsive'
  },
};

